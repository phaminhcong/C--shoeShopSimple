using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
namespace phaminhcongBlazorServer.Data

{
    public class UniqueCategoryNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var categoryName = value.ToString();
                var catIdProperty = validationContext.ObjectType.GetProperty("CatId");

                if (catIdProperty == null)
                {
                    throw new ArgumentException("Không tìm thấy thuộc tính CatId.");
                }

                var catId = (int)catIdProperty.GetValue(validationContext.ObjectInstance, null);

                using (var connection = DBMySQLUltils.GetMySqlConnection())
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM categories WHERE cat_name = @CategoryName AND cat_id != @CatId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryName", categoryName);
                        command.Parameters.AddWithValue("@CatId", catId);
                        var result = Convert.ToInt32(command.ExecuteScalar());

                        if (result > 0)
                        {
                            return new ValidationResult("Tên danh mục đã tồn tại, vui lòng chọn tên khác");
                        }
                    }
                }
            }
            return ValidationResult.Success;
        }
    }

    public class Category
    {
        public int CatId { get; set; }
        [Required(ErrorMessage = "Vui long nhap ten")]
        [UniqueCategoryName(ErrorMessage = "Tên danh mục đã tồn tại, vui lòng chọn tên khác")]
        public string CatName { get; set; }
        [Required]
        [Range(0,1)]
        public int IsDeleted { get; set; }

        public Category()
        {
         
        }
        public Category (int catId, string catName, int isDeleted)
        {
            CatId = catId;
            CatName = catName;
            IsDeleted = isDeleted;
        }
    }
}
