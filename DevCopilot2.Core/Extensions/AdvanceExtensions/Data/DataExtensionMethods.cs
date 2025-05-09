using DevCopilot2.Domain.DTOs.Data;
using DevCopilot2.Domain.Enums.DataTypes;
using Microsoft.Data.SqlClient;

namespace DevCopilot2.Core.Extensions.AdvanceExtensions.Data
{
    public static class DataExtensionMethods
    {
        public static string GetConnectionString(this ConnectionStringListDto? profile)
        {
            if (profile == null)
                return string.Empty;

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            if (!string.IsNullOrWhiteSpace(profile.DataSource))
                builder.DataSource = profile.DataSource;

            if (!string.IsNullOrWhiteSpace(profile.DbName))
                builder.InitialCatalog = profile.DbName;

            if (!string.IsNullOrWhiteSpace(profile.UserName))
                builder.UserID = profile.UserName;

            if (!string.IsNullOrWhiteSpace(profile.Password))
                builder.Password = profile.Password;
            builder.TrustServerCertificate = profile.TrustServerCertificate;
            builder.MultipleActiveResultSets = profile.MultipleActiveResultsSet;
            return builder.ConnectionString;
        }

        public static DataTypeEnum GetDataType(this string clrTypeName)
        {
            return clrTypeName switch
            {
                "Int64" or "bigint" => DataTypeEnum.Int,
                "Int32" or "integer" or "int" => DataTypeEnum.Int,
                "tinyint" => DataTypeEnum.Byte,
                "String" or "nvarchar" or "varchar" => DataTypeEnum.String,
                "Boolean" or "bit" => DataTypeEnum.Bool,
                "Double" => DataTypeEnum.Double,
                "Decimal" => DataTypeEnum.Decimal,
                "Guid" => DataTypeEnum.Guid,
                "DateTime" or "datetime2" => DataTypeEnum.DateTime,
                _ => throw new Exception($"data type Not Found from string {clrTypeName}")
            };
        }
    }
}
