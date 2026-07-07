using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Sellers.Handlers
{
    internal class CreateSellerHandler : ICommandHandler<CreateSellerCommand>
    {
        private const string sellerRoleName = "Seller";
        private const int sellerCodeBase = 600000;

        private readonly ISellerFactory _factory;
        private readonly ISellerRepository _repository;
        private readonly ISellerReadService _readService;
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletFactory _walletFactory;
        private readonly IBuyerReadService _buyerReadService;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        public CreateSellerHandler(ISellerFactory factory, ISellerRepository repository, ISellerReadService readService,
            IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository,
            IBuyerReadService buyerReadService, IWalletRepository walletRepository, IWalletFactory walletFactory)
        {
            _factory = factory;
            _repository = repository;
            _readService = readService;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _buyerReadService = buyerReadService;
            _walletRepository = walletRepository;
            _walletFactory = walletFactory;
        }

        public async Task Handle(CreateSellerCommand request, CancellationToken cancellationToken)
        {
            if (await _readService.ExistsByUserIdAsync(request.UserId))
                return;

            if(await _buyerReadService.ExistsByUserIdAsync(request.UserId))
                throw new BusinessException("اطلاعات شما قبلا به عنوان خریدار ثبت شده است.");

            var user = await _userRepository.GetAsync(u => u.Id == request.UserId);
            if (user is null)
                throw new BusinessException("کاربر یافت نشد.");

            var sellerRole = await _roleRepository.GetAsync(r => r.Name == sellerRoleName);

            var lastCode = await _readService.GetLastCodeAsync();
            var nextCode = Code.Create(lastCode is null || lastCode < sellerCodeBase ? sellerCodeBase + 1 : lastCode.Value + 1);

            var seller = _factory.Create(request.FirstName, request.LastName, request.NationalCode, request.City, request.Province,
                request.PostalCode, request.AddressDescription, request.Email, request.Gender, request.PersonType, user,
                request.NationalCardFileId, request.ProfileFormFileId, nextCode);

            var sellerUserRole = user.AddRole(sellerRole!);


            var wallet = _walletFactory.Create(seller, null);

            await _walletRepository.AddAsync(wallet);

            await _userRoleRepository.AddAsync(sellerUserRole);

            await _repository.AddAsync(seller);
            await _repository.CommitAsync();

        }
    }
}