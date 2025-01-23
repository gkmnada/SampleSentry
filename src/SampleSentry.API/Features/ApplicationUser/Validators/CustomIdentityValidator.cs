using Microsoft.AspNetCore.Identity;

namespace SampleSentry.API.Features.ApplicationUser.Validators
{
    public class CustomIdentityValidator : IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "PasswordTooShort",
                Description = $"Şifreniz En Az {length} Karakter Olmalıdır"
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresUpper",
                Description = "Şifreniz En Az 1 Büyük Harf İçermelidir"
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresLower",
                Description = "Şifreniz En Az 1 Küçük Harf İçermelidir"
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresDigit",
                Description = "Şifreniz En Az 1 Rakam İçermelidir"
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresNonAlphanumeric",
                Description = "Şifreniz En Az 1 Sembol İçermelidir"
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError
            {
                Code = "PasswordMismatch",
                Description = "Geçerli Şifreniz Hatalı"
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "DuplicateUserName",
                Description = "Bu kullanıcı adı zaten kullanılıyor. Lütfen başka bir kullanıcı adı deneyin."
            };
        }

        public override IdentityError InvalidUserName(string? userName)
        {
            return new IdentityError()
            {
                Code = "InvalidUserName",
                Description = "Geçersiz kullanıcı adı. Yalnızca harf ve rakam kullanabilirsiniz."
            };
        }
    }
}
