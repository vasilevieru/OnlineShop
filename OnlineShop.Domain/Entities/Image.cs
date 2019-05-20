namespace OnlineShop.Domain.Entities
{
    public class Image : BaseEntity
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string MimeType { get; set; }
        public long Length { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
