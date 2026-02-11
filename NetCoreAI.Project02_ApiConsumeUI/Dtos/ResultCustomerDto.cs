namespace NetCoreAI.Project02_ApiConsumeUI.Dtos
{
    public class ResultCustomerDto
    {
            // Entities'de Customer'ın C'si büyük geliyor, burada küçük gelmesi bir problem oluşturmaz.
            public int customerId { get; set; }
            public string customerName { get; set; }
            public string customerSurname { get; set; }
            public decimal customerBalance { get; set; }
    }
}
