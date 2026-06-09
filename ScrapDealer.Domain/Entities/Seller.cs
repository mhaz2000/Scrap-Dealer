using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.Profiles;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{


    public class Seller : AggregateRoot<Guid>
    {
        public PersonName PersonName { get; private set; }
        public NationalCode NationalCode { get; private set; }
        public ProfileAddress Address { get; private set; }
        public Email Email { get; private set; }
        public Gender Gender { get; private set; }
        public PersonType PersonType { get; private set; }
        public User User { get; private set; }
        public Guid UserId { get; private set; }

        public bool Verified { get; private set; }
        public bool IsActive { get; private set; }
        public Score Score { get; set; }

        public Guid? ProfileFormFileId { get; private set; }
        public Guid? NationalCardFileId { get; private set; }

        public Seller()
        {

        }

        public Seller(PersonName personName, NationalCode nationalCode,
            ProfileAddress address, Email email, PersonType personType, Gender gender, Guid userId, Guid? nationalCardFileId, Guid? profileFormFileId)
        {
            Id = Guid.NewGuid();
            PersonName = personName;
            NationalCode = nationalCode;
            Address = address;
            Email = email;
            Gender = gender;
            PersonType = personType;
            UserId = userId;
            Verified = false;
            IsActive = true;
            Score = 0;
            NationalCardFileId = nationalCardFileId;
            ProfileFormFileId = profileFormFileId;
        }

        public void Update(PersonName personName, NationalCode nationalCode,
            ProfileAddress address, Email email, PersonType personType, Gender gender, Guid? nationalCardFileId, Guid? profileFormFileId)
        {
            PersonName = personName;
            NationalCode = nationalCode;
            Address = address;
            Email = email;
            Gender = gender;
            PersonType = personType;
            NationalCardFileId = nationalCardFileId;
            ProfileFormFileId = profileFormFileId;
        }

        public void SetAsVerified() => Verified = true;
        public void SetActivationStatus(bool status) => IsActive = status;

    }
}
