using ScrapDealer.Domain.Consts;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class Referral : AggregateRoot<Guid>
    {
        public Guid ReferrerUserId { get; private set; }
        public Guid RefereeUserId { get; private set; }
        public string Code { get; private set; }
        public ReferralStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Referral()
        {
        }

        public Referral(Guid referrerUserId, Guid refereeUserId, string code)
        {
            Id = Guid.NewGuid();
            ReferrerUserId = referrerUserId;
            RefereeUserId = refereeUserId;
            Code = code;
            Status = ReferralStatus.Pending;
            CreatedAt = DateTime.Now;
        }

        public void Approve()
        {
            if (Status == ReferralStatus.Approved)
                return;

            Status = ReferralStatus.Approved;
        }
    }
}
