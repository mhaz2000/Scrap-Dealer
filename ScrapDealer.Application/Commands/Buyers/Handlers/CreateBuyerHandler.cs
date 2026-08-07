using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Buyers.Handlers
{
    internal class CreateBuyerHandler : ICommandHandler<CreateBuyerCommand>
    {
        private const string buyerRoleName = "Buyer";
        private const int buyerCodeBase = 400000;

        private readonly IBuyerFactory _factory;
        private readonly IWalletFactory _walletFactory;
        private readonly IWalletRepository _walletRepository;
        private readonly IBuyerRepository _repository;
        private readonly IBuyerReadService _readService;
        private readonly ISellerReadService _sellerReadService;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        public CreateBuyerHandler(IBuyerFactory factory, IBuyerRepository repository, IBuyerReadService readService,
            IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository,
            ISellerReadService sellerReadService, IWalletFactory walletFactory, IWalletRepository walletRepository)
        {
            _factory = factory;
            _repository = repository;
            _readService = readService;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _sellerReadService = sellerReadService;
            _walletFactory = walletFactory;
            _walletRepository = walletRepository;
        }

        public async Task Handle(CreateBuyerCommand request, CancellationToken cancellationToken)
        {
            if (await _readService.ExistsByUserIdAsync(request.UserId))
                return;

            if(await _sellerReadService.ExistsByUserIdAsync(request.UserId))
                throw new BusinessException("اطلاعات شما قبلا به عنوان فروشنده ثبت شده است.");

            var user = await _userRepository.GetAsync(u => u.Id == request.UserId);
            if (user is null)
                throw new BusinessException("کاربر یافت نشد.");

            var buyerRole = await _roleRepository.GetAsync(r => r.Name == buyerRoleName);

            var lastCode = await _readService.GetLastCodeAsync();
            var nextCode = Code.Create(lastCode is null || lastCode < buyerCodeBase ? buyerCodeBase + 1 : lastCode.Value + 1);

            var buyer = _factory.Create(request.FirstName, request.LastName, request.NationalCode, request.City, request.Province,
                request.CompanyName, request.NumberPlate, request.AddressDescription, request.Gender, request.ActivityArea,
                request.BusinessLicenseFileId, request.NationalCardFileId, request.ProfileFormFileId, request.CarCardFileId,
                request.IsWholeSaleBuyer, request.IsFixedLocation, request.LocationImages, user,
                request.Latitude, request.Longitude, nextCode);

            var buyerUserRole = user.AddRole(buyerRole!);

            var wallet = _walletFactory.Create(null, buyer);

            await _userRoleRepository.AddAsync(buyerUserRole);
            await _repository.AddAsync(buyer);
            await _walletRepository.AddAsync(wallet);

            await _userRepository.CommitAsync();
        }
    }
}
