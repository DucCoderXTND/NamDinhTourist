using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace TND.Application.Extensions.Validation
{
    /// <summary>
    /// Các phương thức mở rộng cho FluentValidation để thêm quy tắc xác thực tùy chỉnh.
    /// </summary>
    public static class ValidationExtensions
    {
        ///<summary>
        ///Xác thực rằng thuộc tính chuỗi đại diện cho một tên hợp lệ 
        ///với độ dài tối thiểu và tối đa được chỉ định.
        /// </summary>
        ///<typeparam name="T">Loại đối tượng được xác thực
        ///</typeparam>
        ///<param name="ruleBuilder">The rule builder.</param>
        /// <param name="minLength">The minimum length of the name.</param>
        /// <param name="maxLength">The maximum length of the name.</param>
        /// <returns>The rule builder options.</returns>
        /// <remarks>
        ///     Mẫu tên hợp lệ cho phép các ký tự chữ cái và dấu cách.
        /// </remarks>
        
        public static IRuleBuilderOptions<T, string> ValidName<T>(
            this IRuleBuilder<T, string> ruleBuilder, int minLength, int maxLength)
        {
            return ruleBuilder
                .Matches(@"^[A-Za-z\s]+$")
                .WithMessage(ValidationMessages.NameIsNotValid)
                .Length(minLength, maxLength);
        }


        /// <summary>
        ///   Validates that the string property represents a valid phone number.
        /// </summary>
        /// 
        public static IRuleBuilderOptions<T, string> PhoneNumber<T>(
    this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")
                .WithMessage(ValidationMessages.PhoneNumberIsNotValid);
        }

        /// <summary>
        ///   phương thức này nhận một tham số là độ dài yêu cầu của chuỗi số 
        ///   và sử dụng biểu thức chính quy để xác minh.
        /// </summary>
        /// 

        public static IRuleBuilderOptions<T, string> ValidNumericString<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        int length)
        {
            return ruleBuilder
                .Matches($"^[0-9]{{{length}}}$")
                .WithMessage(ValidationMessages.GenerateNotANumericStringMessage(length));
        }

        /// <summary>
        ///   Validates that the IFormFile property represents a valid image file.
        /// </summary>


        public static IRuleBuilderOptions<T, IFormFile> ValidImage<T>(
        this IRuleBuilder<T, IFormFile> ruleBuilder)
        {
            var allowedImageTypes = new[] { "image/jpg", "image/jpeg", "image/png" };

            return ruleBuilder
              .Must(x => allowedImageTypes.Contains(x.ContentType, StringComparer.OrdinalIgnoreCase))
              .WithMessage(ValidationMessages.NotAnImageFile);
        }

        /// <summary>
        ///   Validates that the string property represents a strong password.
        /// </summary>
        /// 

        /// <remarks>
        ///   The strong password pattern enforces a combination of uppercase and lowercase letters,
        ///   digits, and special characters, with a minimum length of 8 characters.
        /// </remarks>
        /// 

        public static IRuleBuilderOptions<T, string> StrongPassword<T>(this IRuleBuilderOptions<T, string> ruleBuilder)
        {
            return ruleBuilder
              .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
              .WithMessage(ValidationMessages.PasswordIsWeak);
        }

    }
}
