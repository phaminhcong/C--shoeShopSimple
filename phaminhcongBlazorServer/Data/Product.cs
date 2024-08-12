using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

namespace phaminhcongBlazorServer.Data
{
    public class UniqueProductNameAttribute : ValidationAttribute
    {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    var productName = value.ToString();
                    var productIdProperty = validationContext.ObjectType.GetProperty("ProductId");

                    if (productIdProperty == null)
                    {
                        throw new ArgumentException("Không tìm thấy thuộc tính ProductId.");
                    }

                    var productId = (int)productIdProperty.GetValue(validationContext.ObjectInstance, null);

                    using (var connection = DBMySQLUltils.GetMySqlConnection())
                    {
                        connection.Open();
                        string query = "SELECT COUNT(*) FROM products WHERE prd_name = @ProductName AND prd_id != @ProductId";
                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ProductName", productName);
                            command.Parameters.AddWithValue("@ProductId", productId);
                            var result = Convert.ToInt32(command.ExecuteScalar());

                            if (result > 0)
                            {
                                return new ValidationResult("Tên sản phẩm đã tồn tại, vui lòng chọn tên khác");
                            }
                        }
                    }
                }
                return ValidationResult.Success;
            }
        
    }
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [UniqueProductName(ErrorMessage = "Tên sản phẩm đã tồn tại, vui lòng chọn tên khác")]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [Range(1, int.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn 0")]
        public int ProductPrice { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng sản phẩm phải lớn hơn 0")]
        public int ProductQuantity { get; set; } = 0;
        
        public string ProductImage { get; set; }
        [Required(ErrorMessage = "Danh mục không được để trống")]
        public int ProductCategoryId { get; set;}
        [Required(ErrorMessage = "Chọn trạng thái")]
        [Range(0, 1)]
        public int IsDeleted { get; set; }
        
        public Product()
        {

        }
        public Product(int productId, string productName, string productDescription, int productPrice ,int productQuantity, string productImage , int productCategoryId ,int isDeleted)
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
            ProductQuantity = productQuantity;
            ProductImage = productImage;
            ProductCategoryId = productCategoryId;
            IsDeleted = isDeleted;
        }

    }
}
