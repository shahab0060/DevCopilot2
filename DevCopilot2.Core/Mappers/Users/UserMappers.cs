using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.MediasInformation.Users;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Users;
using DevCopilot2.Domain.Entities.Users;

namespace DevCopilot2.Core.Mappers.Users
{
    public static class UserMappers
    {
        public static UserListDto ToDto(this User user)
          => new UserListDto()
          {
              Id = user.Id,
              LatestEditDate = user.LatestEditDate,
              CreateDate = user.CreateDate,
              EditCounts = user.EditCounts,
              IsSuperAdmin = user.IsSuperAdmin,

              ImageName = user.ImageName,

              FirstName = user.FirstName,

              LastName = user.LastName,

              PhoneNumber = user.PhoneNumber,

              Password = user.Password,

              RoleIds = user.UserSelectedRoles?
           .Select(d => d.RoleId)
           .ToList() ?? new List<long>()
          };

        public static UpdateUserDto ToUpdateDto(this User user)
     => new UpdateUserDto()
     {
         Id = user.Id,

         IsSuperAdmin = user.IsSuperAdmin,

         ImageName = user.ImageName,

         FirstName = user.FirstName,

         LastName = user.LastName,

         PhoneNumber = user.PhoneNumber,

         Password = user.Password,

         RoleIds = user.UserSelectedRoles
           .Select(d => d.RoleId)
           .ToList()
     };

        public static ComboDto ToCombo(this User user)
              => new ComboDto()
              { Title = user.FirstName, Value = user.Id.ToString() };

        public static IQueryable<UserListDto> ToDto(this IQueryable<User> Users)
              => Users.Select(a => a.ToDto());

        public static IQueryable<ComboDto> ToCombo(this IQueryable<User> Users)
              => Users.Select(a => a.ToCombo());

        public static async Task<User> ToModel(this CreateUserDto create)
                  => new User()
                  {

                      IsSuperAdmin = create.IsSuperAdmin,
                      ImageName = await create.Image.UploadImageAsync(
                            UserMediaInformation.ImageMediasInformation,
                            Guid.NewGuid().ToString().ToUrl(), 0, null) ?? "",

                      FirstName = create.FirstName.ToTitle()!,

                      LastName = create.LastName.ToTitle()!,

                      PhoneNumber = create.PhoneNumber,

                      Password = create.Password,
                      PhoneNumberActivationCode = "",
                      PhoneNumberActivationCodeExpireTime = DateTime.Now,
                  };

        public static async Task<User> ToModel(this User user, UpdateUserDto update)
        {

            user.IsSuperAdmin = update.IsSuperAdmin;
            if (update.Image is not null)
                user.ImageName = await update.Image.UploadImageAsync(
            UserMediaInformation.ImageMediasInformation,
            Guid.NewGuid().ToString(), user.EditCounts + 1, user.ImageName) ?? "";

            user.FirstName = update.FirstName.ToTitle()!;

            user.LastName = update.LastName.ToTitle()!;

            user.PhoneNumber = update.PhoneNumber;

            user.Password = update.Password;
            return user;
        }

        public static async Task<User> SendPhoneNumberOtpCode(this User user)
        {
            Random random = new Random();
            user.PhoneNumberActivationCodeExpireTime = DateTime.Now.AddMinutes(2);
            user.PhoneNumberActivationCode = random.Next(1000, 9999).ToString();
            return user;
        }
    }
}
