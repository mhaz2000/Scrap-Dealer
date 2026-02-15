namespace ScrapDealer.Domain.Consts
{
    public enum ContractStatus
    {
        AcceptByBuyer,
        CancelledBySeller,
        CancelledByBuyer,
        AcceptBySeller,
        AmountConfirmed,
        AdminConfirmed,
        PendingForCommission,
        Done
    }
}
