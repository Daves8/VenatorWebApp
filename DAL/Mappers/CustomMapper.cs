using Dapper;
using VenatorWebApp.Models;

namespace VenatorWebApp.DAL.Mapper
{
    public static class CustomMapper
    {
        public static CustomPropertyTypeMap GetMapperByType(Type type)
        {
            return type switch
            {
                Type t when t == typeof(User) => GetUserMapper(),
                Type t when t == typeof(Item) => GetItemMapper(),
                Type t when t == typeof(Message) => GetMessageMapper(),
                Type t when t == typeof(News) => GetNewsMapper(),
                Type t when t == typeof(Topic) => GetTopicMapper(),
                Type t when t == typeof(Comment) => GetCommentMapper(),
                Type t when t == typeof(Statistics) => GetStatisticsMapper(),
                _ => throw new ArgumentException($"Custom mapper for type = {type} not found"),
            };
        }

        public static CustomPropertyTypeMap GetUserMapper()
        {
            return new CustomPropertyTypeMap(typeof(User), (type, columnName) =>
            {
                return columnName switch
                {
                    "ID" => type.GetProperty("Id"),
                    "USER_NAME" => type.GetProperty("Name"),
                    "PASSWORD" => type.GetProperty("Password"),
                    "FULL_NAME" => type.GetProperty("FullName"),
                    "EMAIL" => type.GetProperty("Email"),
                    "PHONE_NUMBER" => type.GetProperty("PhoneNumber"),
                    "IMAGE_URL" => type.GetProperty("ImageUrl"),
                    "ROLE" => type.GetProperty("Role"),
                    "GOLD_AMOUNT" => type.GetProperty("GoldAmount"),
                    "CREATION_DATE" => type.GetProperty("CreationDate"),
                    _ => null,
                };
            });
        }

        public static CustomPropertyTypeMap GetItemMapper()
        {
            return new CustomPropertyTypeMap(typeof(Item), (type, columnName) =>
            {
                return columnName switch
                {
                    "ID" => type.GetProperty("Id"),
                    "NAME" => type.GetProperty("Name"),
                    "DESCRIPTION" => type.GetProperty("Description"),
                    "CATEGORY" => type.GetProperty("Category"),
                    "PRICE" => type.GetProperty("Price"),
                    "IS_HIDDEN" => type.GetProperty("IsHidden"),
                    "IMAGE_URL" => type.GetProperty("ImageUrl"),
                    "CREATION_DATE" => type.GetProperty("CreationDate"),
                    _ => null,
                };
            });
        }

        public static CustomPropertyTypeMap GetMessageMapper()
        {
            return new CustomPropertyTypeMap(typeof(Message), (type, columnName) =>
            {
                return columnName switch
                {
                    "ID" => type.GetProperty("Id"),
                    "TEXT" => type.GetProperty("Text"),
                    "FROM_USER_ID" => type.GetProperty("OwnerId"),
                    // This is does not work on current configuration
                    //return type.GetProperty("Owner").PropertyType.GetProperty("Id");
                    "TO_USER_ID" => type.GetProperty("ToUserId"),
                    //TODO: add support
                    /*case "PARENT_MESSAGE_ID":
                        return type.GetProperty("Parent");*/
                    "IS_HIDDEN" => type.GetProperty("IsHidden"),
                    "CREATION_DATE" => type.GetProperty("CreationDate"),
                    _ => null,
                };
            });
        }

        public static CustomPropertyTypeMap GetNewsMapper()
        {
            return new CustomPropertyTypeMap(typeof(News), (type, columnName) =>
            {
                return columnName switch
                {
                    "ID" => type.GetProperty("Id"),
                    "TITLE" => type.GetProperty("Name"),
                    "TEXT" => type.GetProperty("Text"),
                    "METRICS" => type.GetProperty("Metrics"),
                    "USER_ID" => type.GetProperty("OwnerId"),
                    "LIKES_COUNT" => type.GetProperty("LikesCount"),
                    "DISLIKES_COUNT" => type.GetProperty("DislikesCount"),
                    "IS_HIDDEN" => type.GetProperty("IsHidden"),
                    "CREATION_DATE" => type.GetProperty("CreationDate"),
                    _ => null,
                };
            });
        }

        public static CustomPropertyTypeMap GetTopicMapper()
        {
            return new CustomPropertyTypeMap(typeof(Topic), (type, columnName) =>
            {
                return columnName switch
                {
                    "ID" => type.GetProperty("Id"),
                    "TITLE" => type.GetProperty("Name"),
                    "TEXT" => type.GetProperty("Text"),
                    "METRICS" => type.GetProperty("Metrics"),
                    "USER_ID" => type.GetProperty("OwnerId"),
                    "LIKES_COUNT" => type.GetProperty("LikesCount"),
                    "DISLIKES_COUNT" => type.GetProperty("DislikesCount"),
                    "IS_HIDDEN" => type.GetProperty("IsHidden"),
                    "CREATION_DATE" => type.GetProperty("CreationDate"),
                    _ => null,
                };
            });
        }

        public static CustomPropertyTypeMap GetCommentMapper()
        {
            return new CustomPropertyTypeMap(typeof(Comment), (type, columnName) =>
            {
                return columnName switch
                {
                    "ID" => type.GetProperty("Id"),
                    "TEXT" => type.GetProperty("Text"),
                    "PARENT_TYPE_ID" => type.GetProperty("ParentType"),
                    "PARENT_ID" => type.GetProperty("ParentId"),
                    "USER_ID" => type.GetProperty("OwnerId"),
                    "LIKES_COUNT" => type.GetProperty("LikesCount"),
                    "DISLIKES_COUNT" => type.GetProperty("DislikesCount"),
                    "IS_HIDDEN" => type.GetProperty("IsHidden"),
                    "CREATION_DATE" => type.GetProperty("CreationDate"),
                    _ => null,
                };
            });
        }

        public static CustomPropertyTypeMap GetStatisticsMapper()
        {
            return new CustomPropertyTypeMap(typeof(Statistics), (type, columnName) =>
            {
                return columnName switch
                {
                    "ID" => type.GetProperty("Id"),
                    "USER_ID" => type.GetProperty("OwnerId"),
                    "COMPLETED_QUESTS_COUNTER" => type.GetProperty("CompletedQuestsCounter"),
                    "KILLED_PLAYERS_COUNTER" => type.GetProperty("KilledPlayersCounter"),
                    "KILLED_NPC_COUNTER" => type.GetProperty("KilledNpcCounter"),
                    "KILLED_ANIMALS_COUNTER" => type.GetProperty("KilledAnimalsCounter"),
                    "DEATH_FROM_PLAYERS_COUNTER" => type.GetProperty("DeathFromPlayersCounter"),
                    "DEATH_FROM_NPC_COUNTER" => type.GetProperty("DeathFromNpcCounter"),
                    "DEATH_FROM_ANIMALS_COUNTER" => type.GetProperty("DeathFromAnimalsCounter"),
                    _ => null,
                };
            });
        }
    }
}