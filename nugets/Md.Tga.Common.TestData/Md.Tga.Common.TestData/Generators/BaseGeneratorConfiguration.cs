namespace Md.Tga.Common.TestData.Generators
{
    using System;

    public class BaseGeneratorConfiguration
    {
        public DateTime? Created { get; set; } = DateTime.Now;
        public string? DocumentId { get; set; } = Guid.NewGuid().ToString();

        public string? ParentDocumentId { get; set; } = Guid.NewGuid().ToString();
    }
}
