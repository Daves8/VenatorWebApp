using Dapper;
using VenatorWebApp.Models;

namespace VenatorWebApp.DAL.Mapper
{
    public static class CustomMapper
    {
        public static CustomPropertyTypeMap GetMapperByType(Type type)
        {
            switch (type)
            {
                case Type t when t == typeof(User):
                    return GetUserMapper();
                case Type t when t == typeof(Item):
                    return GetItemMapper();
                default:
                    throw new ArgumentException($"Custom mapper for type = {type} not found.");
            }
        }


        /*
         
         
         CREATE TABLE USERS (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    USER_NAME VARCHAR(50) NOT NULL,
    PASSWORD VARCHAR(50) NOT NULL,
    FULL_NAME VARCHAR(100),
	EMAIL VARCHAR(50),
	PHONE_NUMBER VARCHAR(50),
	IMAGE_URL VARCHAR(50),
	ROLE INT,
	GOLD_AMOUNT DECIMAL(10,2),
	CREATION_DATE DATETIME
);
         
         
         
         */


        public static CustomPropertyTypeMap GetUserMapper()
        {

            return new CustomPropertyTypeMap(typeof(User), (type, columnName) =>
            {
                switch (columnName)
                {
                    case "ID": return type.GetProperty("Id");
                    case "USER_NAME": return type.GetProperty("Name");
                    case "PASSWORD": return type.GetProperty("Password");
                    case "FULL_NAME": return type.GetProperty("FullName");
                    case "EMAIL": return type.GetProperty("Email");
                    case "PHONE_NUMBER": return type.GetProperty("PhoneNumber");
                    case "IMAGE_URL": return type.GetProperty("ImageUrl");
                    case "ROLE": return type.GetProperty("Role");
                    case "GOLD_AMOUNT": return type.GetProperty("GoldAmount");
                    case "CREATION_DATE": return type.GetProperty("CreationDate");
                    default: return null;
                }


                /*if (columnName.Equals("id", StringComparison.OrdinalIgnoreCase))
                    return type.GetProperty("id");
                else if (columnName.Equals("UserName", StringComparison.OrdinalIgnoreCase))
                    return type.GetProperty("Name");
                else if (columnName.Equals("password", StringComparison.OrdinalIgnoreCase))
                    return type.GetProperty("password");
                else if (columnName.Equals("Role", StringComparison.OrdinalIgnoreCase))
                    return type.GetProperty("Role");
                else if (columnName.Equals("fullname", StringComparison.OrdinalIgnoreCase))
                    return type.GetProperty("fullname");
                else if (columnName.Equals("GoldAmount", StringComparison.OrdinalIgnoreCase))
                    return type.GetProperty("GoldAmount");
                else if (columnName.Equals("CreationDate", StringComparison.OrdinalIgnoreCase))
                    return type.GetProperty("CreationDate");

                return null;*/
            });
        }

        public static CustomPropertyTypeMap GetItemMapper()
        {

            return new CustomPropertyTypeMap(typeof(User), (type, columnName) =>
            {
                if (columnName.Equals("id", StringComparison.OrdinalIgnoreCase))
                    return type.GetProperty("id");
                else if (columnName.Equals("username", StringComparison.OrdinalIgnoreCase))
                    return type.GetProperty("name");
                else if (columnName.Equals("password", StringComparison.OrdinalIgnoreCase))
                    return type.GetProperty("password");

                return null;
            });
        }
    }
}