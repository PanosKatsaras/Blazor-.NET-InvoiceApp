﻿namespace InvoiceApp.DTOS
{
    //InvoiceTerms data transfer object
    public class InvoiceTermsDTO : IDTO, IOwnedDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = String.Empty;
        public string UserId { get; set; } = null!;
    }
}
