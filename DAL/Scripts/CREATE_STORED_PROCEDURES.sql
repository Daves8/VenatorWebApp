
-- USERS
CREATE PROCEDURE [DBO].[CREATE_USER]
	@USER_NAME VARCHAR(50),
	@PASSWORD VARCHAR(100),
	@FULL_NAME VARCHAR(100),
	@EMAIL VARCHAR(50),
	@PHONE_NUMBER VARCHAR(50),
	@IMAGE_URL VARCHAR(50)
AS
BEGIN
	INSERT INTO DBO.USERS (USER_NAME, PASSWORD, FULL_NAME, EMAIL, PHONE_NUMBER, IMAGE_URL, ROLE, GOLD_AMOUNT, CREATION_DATE)
	VALUES (@USER_NAME, @PASSWORD, @FULL_NAME, @EMAIL, @PHONE_NUMBER, @IMAGE_URL, 0, 0, GETDATE())
END
GO

CREATE PROCEDURE [DBO].[QUERY_USERS]
AS
BEGIN
	SELECT ID, USER_NAME, PASSWORD, FULL_NAME, EMAIL, PHONE_NUMBER, IMAGE_URL, ROLE, GOLD_AMOUNT, CREATION_DATE
	FROM DBO.USERS
END
GO

CREATE PROCEDURE [DBO].[QUERY_USER_BY_ID]
	@ID INT
AS
BEGIN
	SELECT ID, USER_NAME, PASSWORD, FULL_NAME, EMAIL, PHONE_NUMBER, IMAGE_URL, ROLE, GOLD_AMOUNT, CREATION_DATE
	FROM DBO.USERS
	WHERE ID = @ID
END
GO

CREATE PROCEDURE [DBO].[QUERY_USER_BY_USER_NAME]
	@USER_NAME VARCHAR(50)
AS
BEGIN
	SELECT ID, USER_NAME, PASSWORD, FULL_NAME, EMAIL, PHONE_NUMBER, IMAGE_URL, ROLE, GOLD_AMOUNT, CREATION_DATE
	FROM DBO.USERS
	WHERE USER_NAME = @USER_NAME
END
GO

CREATE PROCEDURE [DBO].[QUERY_USER_BY_EMAIL]
	@EMAIL VARCHAR(50)
AS
BEGIN
	SELECT ID, USER_NAME, PASSWORD, FULL_NAME, EMAIL, PHONE_NUMBER, IMAGE_URL, ROLE, GOLD_AMOUNT, CREATION_DATE
	FROM DBO.USERS
	WHERE EMAIL = @EMAIL
END
GO

CREATE PROCEDURE [DBO].[UPDATE_USER]
	@ID INT,
	@USER_NAME VARCHAR(50),
	@PASSWORD VARCHAR(50),
	@FULL_NAME VARCHAR(100),
	@EMAIL VARCHAR(50),
	@PHONE_NUMBER VARCHAR(50),
	@IMAGE_URL VARCHAR(50),
	@ROLE INT,
	@GOLD_AMOUNT DECIMAL(10,2)
AS
BEGIN
	UPDATE DBO.USERS
	SET
		USER_NAME = @USER_NAME,
		PASSWORD = @PASSWORD,
		FULL_NAME = @FULL_NAME,
		EMAIL = @EMAIL,
		PHONE_NUMBER = @PHONE_NUMBER,
		IMAGE_URL = @IMAGE_URL,
		ROLE = @ROLE,
		GOLD_AMOUNT = @GOLD_AMOUNT
	WHERE ID = @ID
END
GO

CREATE PROCEDURE [DBO].[DELETE_USER]
	@ID INT
AS
BEGIN
	DELETE FROM DBO.USERS WHERE ID = @ID
END
GO

CREATE PROCEDURE [DBO].[QUERY_USER_STATISTICS]
	@USER_ID INT
AS
BEGIN
	SELECT ID, USER_ID, COMPLETED_QUESTS_COUNTER, KILLED_PLAYERS_COUNTER, KILLED_NPC_COUNTER, KILLED_ANIMALS_COUNTER, DEATH_FROM_PLAYERS_COUNTER, DEATH_FROM_NPC_COUNTER, DEATH_FROM_ANIMALS_COUNTER
	FROM DBO.STATS
	WHERE USER_ID = @USER_ID
END
GO

CREATE PROCEDURE [DBO].[INIT_USER_STATISTICS]
	@USER_ID INT
AS
BEGIN
	INSERT INTO DBO.STATS (USER_ID, COMPLETED_QUESTS_COUNTER, KILLED_PLAYERS_COUNTER, KILLED_NPC_COUNTER, KILLED_ANIMALS_COUNTER, DEATH_FROM_PLAYERS_COUNTER, DEATH_FROM_NPC_COUNTER, DEATH_FROM_ANIMALS_COUNTER)
	VALUES (@USER_ID, 0, 0, 0, 0, 0, 0, 0)
END
GO

CREATE PROCEDURE [DBO].[CHECK_USER_CREDENTIALS]
	@USER_NAME VARCHAR(50),
	@PASSWORD VARCHAR(50)
AS
BEGIN
	SELECT ID, USER_NAME, PASSWORD, FULL_NAME, EMAIL, PHONE_NUMBER, IMAGE_URL, ROLE, GOLD_AMOUNT, CREATION_DATE
	FROM DBO.USERS
	WHERE USER_NAME COLLATE LATIN1_GENERAL_CS_AS = @USER_NAME COLLATE LATIN1_GENERAL_CS_AS
	AND PASSWORD COLLATE LATIN1_GENERAL_CS_AS = @PASSWORD COLLATE LATIN1_GENERAL_CS_AS
END
GO

CREATE PROCEDURE [DBO].[CHECK_USER_EXISTENCE]
	@USER_NAME VARCHAR(50),
	@EMAIL VARCHAR(50)
AS
BEGIN
	SELECT 1
	FROM DBO.USERS
	WHERE USER_NAME = @USER_NAME
	OR EMAIL = @EMAIL
END
GO

-- ITEMS
CREATE PROCEDURE [DBO].[CREATE_ITEM]
	@NAME VARCHAR(50),
	@DESCRIPTION VARCHAR(255),
	@CATEGORY INT,
	@PRICE DECIMAL(10,2),
	@IS_HIDDEN BIT,
	@IMAGE_URL VARCHAR(50)
AS
BEGIN
	INSERT INTO DBO.ITEMS (NAME, DESCRIPTION, CATEGORY, PRICE, IS_HIDDEN, IMAGE_URL, CREATION_DATE)
	VALUES (@NAME, @DESCRIPTION, @CATEGORY, @PRICE, @IS_HIDDEN, @IMAGE_URL, GETDATE())
END
GO

CREATE PROCEDURE [DBO].[QUERY_ITEM_BY_ID]
	@ID INT
AS
BEGIN
	SELECT ID, NAME, DESCRIPTION, CATEGORY, PRICE, IS_HIDDEN, IMAGE_URL, CREATION_DATE
	FROM DBO.ITEMS
	WHERE ID = @ID
END
GO

CREATE PROCEDURE [DBO].[QUERY_ITEMS]
AS
BEGIN
	SELECT ID, NAME, DESCRIPTION, CATEGORY, PRICE, IS_HIDDEN, IMAGE_URL, CREATION_DATE
	FROM DBO.ITEMS
	ORDER BY CREATION_DATE DESC
END
GO

CREATE PROCEDURE [DBO].[QUERY_ITEMS_BY_IS_HIDDEN]
	@IS_HIDDEN BIT
AS
BEGIN
	SELECT ID, NAME, DESCRIPTION, CATEGORY, PRICE, IS_HIDDEN, IMAGE_URL, CREATION_DATE
	FROM DBO.ITEMS
	WHERE IS_HIDDEN = @IS_HIDDEN
	ORDER BY CREATION_DATE DESC
END
GO

CREATE PROCEDURE [DBO].[UPDATE_ITEM]
	@ID INT,
	@NAME VARCHAR(50),
	@DESCRIPTION VARCHAR(255),
	@CATEGORY INT,
	@PRICE DECIMAL(10,2),
	@IS_HIDDEN BIT,
	@IMAGE_URL VARCHAR(50)
AS
BEGIN
	UPDATE DBO.ITEMS
	SET
		NAME = @NAME,
		DESCRIPTION = @DESCRIPTION,
		CATEGORY = @CATEGORY,
		PRICE = @PRICE,
		IS_HIDDEN = @IS_HIDDEN,
		IMAGE_URL = @IMAGE_URL
	WHERE ID = @ID
END
GO

CREATE PROCEDURE [DBO].[DELETE_ITEM]
	@ID INT
AS
BEGIN
	DELETE FROM DBO.ITEMS
	WHERE ID = @ID
END
GO

CREATE PROCEDURE [DBO].[ADD_ITEM_IN_USER]
	@USER_ID INT,
	@ITEM_ID INT,
	@IN_CART BIT
AS
BEGIN
	INSERT INTO DBO.ITEMS_IN_USER (USER_ID, ITEM_ID, IN_CART, CREATION_DATE)
	VALUES (@USER_ID, @ITEM_ID, @IN_CART, GETDATE())
END
GO

CREATE PROCEDURE [DBO].[QUERY_ITEMS_IN_USER]
	@USER_ID INT,
	@IN_CART BIT
AS
BEGIN
	SELECT I.ID, I.NAME, I.DESCRIPTION, I.CATEGORY, I.PRICE, I.IS_HIDDEN, I.IMAGE_URL, I.CREATION_DATE
	FROM DBO.ITEMS I JOIN DBO.ITEMS_IN_USER U
		ON I.ID = U.ITEM_ID
		AND USER_ID = @USER_ID
		AND IN_CART = @IN_CART
	ORDER BY CATEGORY DESC
END
GO

CREATE PROCEDURE [DBO].[DELETE_ITEMS_IN_USER]
	@USER_ID INT,
	@IN_CART BIT
AS
BEGIN
	DELETE FROM DBO.ITEMS_IN_USER
	WHERE USER_ID = @USER_ID AND IN_CART = @IN_CART
END
GO

CREATE PROCEDURE [DBO].[DELETE_ITEM_IN_USER]
	@USER_ID INT,
	@ITEM_ID INT,
	@IN_CART BIT
AS
BEGIN
	DELETE TOP (1) FROM DBO.ITEMS_IN_USER
	WHERE USER_ID = @USER_ID AND ITEM_ID = @ITEM_ID AND IN_CART = @IN_CART
END
GO

-- need to rework: if the items_in_user table has several items with the same id, then only one item will be bought
CREATE PROCEDURE [DBO].[UPDATE_ITEM_IN_USER]
	@USER_ID INT,
	@ITEM_ID INT,
	@IN_CART BIT
AS
BEGIN
	UPDATE TOP (1) DBO.ITEMS_IN_USER
	SET IN_CART = @IN_CART
	WHERE USER_ID = @USER_ID AND ITEM_ID = @ITEM_ID
END
GO

CREATE PROCEDURE [DBO].[BUY_ALL_ITEMS_IN_CART]
	@USER_ID INT
AS
BEGIN
	UPDATE DBO.ITEMS_IN_USER
	SET IN_CART = 0
	WHERE USER_ID = @USER_ID
END
GO

-- MESSAGE
CREATE PROCEDURE [DBO].[CREATE_MESSAGE]
	@TEXT TEXT,
	@FROM_USER_ID INT,
	@TO_USER_ID INT,
	@PARENT_MESSAGE_ID INT,
	@IS_HIDDEN BIT
AS
BEGIN
	INSERT INTO DBO.MESSAGES (TEXT, FROM_USER_ID, TO_USER_ID, PARENT_MESSAGE_ID, IS_HIDDEN, CREATION_DATE)
	VALUES (@TEXT, @FROM_USER_ID, @TO_USER_ID, @PARENT_MESSAGE_ID, @IS_HIDDEN, GETDATE())
END
GO

CREATE PROCEDURE [DBO].[QUERY_MESSAGE_BY_ID]
	@ID INT
AS
BEGIN
	SELECT ID, TEXT, FROM_USER_ID, TO_USER_ID, PARENT_MESSAGE_ID, IS_HIDDEN, CREATION_DATE
	FROM DBO.MESSAGES
	WHERE ID = @ID
END
GO

CREATE PROCEDURE [DBO].[UPDATE_MESSAGE]
	@ID INT,
	@TEXT TEXT,
	@FROM_USER_ID INT,
	@TO_USER_ID INT,
	@PARENT_MESSAGE_ID INT,
	@IS_HIDDEN BIT
AS
BEGIN
	UPDATE DBO.MESSAGES
	SET
		TEXT = @TEXT,
		FROM_USER_ID = @FROM_USER_ID,
		TO_USER_ID = @TO_USER_ID,
		PARENT_MESSAGE_ID = @PARENT_MESSAGE_ID, 
		IS_HIDDEN = @IS_HIDDEN
	WHERE ID = @ID
END
GO

CREATE PROCEDURE [DBO].[DELETE_MESSAGE]
	@ID INT
AS
BEGIN
	DELETE FROM DBO.MESSAGES WHERE ID = @ID
END
GO

CREATE PROCEDURE [DBO].[QUERY_MESSAGES_BETWEEN_USERS]
	@USER_ONE_ID INT,
	@USER_TWO_ID INT
AS
BEGIN
	SELECT ID, TEXT, FROM_USER_ID, TO_USER_ID, PARENT_MESSAGE_ID, IS_HIDDEN, CREATION_DATE
	FROM DBO.MESSAGES
	WHERE (FROM_USER_ID = @USER_ONE_ID AND TO_USER_ID = @USER_TWO_ID)
		OR (FROM_USER_ID = @USER_TWO_ID AND TO_USER_ID = @USER_ONE_ID) ORDER BY CREATION_DATE DESC
END
GO

CREATE PROCEDURE [DBO].[QUERY_MESSAGES_WITH_USER]
	@USER_ID INT
AS
BEGIN
	SELECT ID, TEXT, FROM_USER_ID, TO_USER_ID, PARENT_MESSAGE_ID, IS_HIDDEN, CREATION_DATE
	FROM DBO.MESSAGES
	WHERE FROM_USER_ID = @USER_ID OR TO_USER_ID = @USER_ID
END
GO

-- CONTENT
CREATE PROCEDURE [DBO].[CREATE_COMMENT]
	@TEXT TEXT,
	@PARENT_TYPE_ID INT,
	@PARENT_ID INT,
	@USER_ID INT,
	@LIKES_COUNT INT,
	@DISLIKES_COUNT INT,
	@IS_HIDDEN BIT
AS
BEGIN
	INSERT INTO DBO.COMMENTS (TEXT, PARENT_TYPE_ID, PARENT_ID, USER_ID, LIKES_COUNT, DISLIKES_COUNT, IS_HIDDEN, CREATION_DATE)
	VALUES (@TEXT, @PARENT_TYPE_ID, @PARENT_ID, @USER_ID, @LIKES_COUNT, @DISLIKES_COUNT, @IS_HIDDEN, GETDATE())
END
GO

CREATE PROCEDURE [DBO].[CREATE_NEWS]
	@TITLE VARCHAR(200),
	@TEXT TEXT,
	@METRICS VARCHAR(50),
	@USER_ID INT,
	@LIKES_COUNT INT,
	@DISLIKES_COUNT INT,
	@IS_HIDDEN BIT
AS
BEGIN
	INSERT INTO DBO.NEWS (TITLE, TEXT, METRICS, USER_ID, LIKES_COUNT, DISLIKES_COUNT, IS_HIDDEN, CREATION_DATE)
	VALUES (@TITLE, @TEXT, @METRICS, @USER_ID, @LIKES_COUNT, @DISLIKES_COUNT, @IS_HIDDEN, GETDATE())
END
GO

CREATE PROCEDURE [DBO].[CREATE_TOPIC]
	@TITLE VARCHAR(200),
	@TEXT TEXT,
	@METRICS VARCHAR(50),
	@USER_ID INT,
	@LIKES_COUNT INT,
	@DISLIKES_COUNT INT,
	@IS_HIDDEN BIT
AS
BEGIN
	INSERT INTO DBO.TOPICS (TITLE, TEXT, METRICS, USER_ID, LIKES_COUNT, DISLIKES_COUNT, IS_HIDDEN, CREATION_DATE)
	VALUES (@TITLE, @TEXT, @METRICS, @USER_ID, @LIKES_COUNT, @DISLIKES_COUNT, @IS_HIDDEN, GETDATE())
END
GO

CREATE PROCEDURE [DBO].[CREATE_REACTION]
	@REACTION_TYPE INT,
	@TEXTUAL_ID INT,
	@TEXTUAL_TYPE INT,
	@USER_ID INT
AS
BEGIN
	INSERT INTO DBO.REACTIONS (REACTION_TYPE, TEXTUAL_ID, TEXTUAL_TYPE, USER_ID, CREATION_DATE)
	VALUES (@REACTION_TYPE, @TEXTUAL_ID, @TEXTUAL_TYPE, @USER_ID, GETDATE())
END
GO

CREATE PROCEDURE [DBO].[CHECK_REACTION_EXISTENCE]
	@REACTION_TYPE INT,
	@TEXTUAL_ID INT,
	@TEXTUAL_TYPE INT,
	@USER_ID INT
AS
BEGIN
	SELECT 1
	FROM DBO.REACTIONS
	WHERE TEXTUAL_ID = @TEXTUAL_ID AND TEXTUAL_TYPE = @TEXTUAL_TYPE AND USER_ID = @USER_ID AND REACTION_TYPE = @REACTION_TYPE
END
GO

CREATE PROCEDURE [DBO].[DELETE_COMMENT]
	@ID INT
AS
BEGIN
	DELETE FROM DBO.COMMENTS WHERE ID = @ID
END
GO

CREATE PROCEDURE [DBO].[DELETE_NEWS]
	@ID INT
AS
BEGIN
	DELETE FROM DBO.NEWS WHERE ID = @ID
END
GO

CREATE PROCEDURE [DBO].[DELETE_TOPIC]
	@ID INT
AS
BEGIN
	DELETE FROM DBO.TOPICS WHERE ID = @ID
END
GO

CREATE PROCEDURE [DBO].[DELETE_REACTION]
	@TEXTUAL_ID INT,
	@TEXTUAL_TYPE INT,
	@USER_ID INT
AS
BEGIN
	DELETE FROM DBO.REACTIONS
	WHERE TEXTUAL_ID = @TEXTUAL_ID AND TEXTUAL_TYPE = @TEXTUAL_TYPE AND USER_ID = @USER_ID
END
GO

CREATE PROCEDURE [DBO].[UPDATE_REACTION]
	@REACTION_TYPE INT,
	@TEXTUAL_ID INT,
	@TEXTUAL_TYPE INT,
	@USER_ID INT
AS
BEGIN
	UPDATE DBO.REACTIONS
	SET REACTION_TYPE = @REACTION_TYPE
	WHERE TEXTUAL_ID = @TEXTUAL_ID AND TEXTUAL_TYPE = @TEXTUAL_TYPE AND USER_ID = @USER_ID
END
GO

CREATE PROCEDURE [DBO].[QUERY_COMMENTS]
	@PARENT_ID INT
AS
BEGIN
	SELECT ID, TEXT, PARENT_TYPE_ID, PARENT_ID, USER_ID, LIKES_COUNT, DISLIKES_COUNT, IS_HIDDEN, CREATION_DATE
	FROM DBO.COMMENTS
	WHERE PARENT_ID = @PARENT_ID
	ORDER BY CREATION_DATE DESC
END
GO

CREATE PROCEDURE [DBO].[QUERY_NEWS]
AS
BEGIN
	SELECT ID, TITLE, TEXT, METRICS, USER_ID, LIKES_COUNT, DISLIKES_COUNT, IS_HIDDEN, CREATION_DATE
	FROM DBO.NEWS
	ORDER BY CREATION_DATE DESC
END
GO

CREATE PROCEDURE [DBO].[QUERY_TOPICS]
AS
BEGIN
	SELECT ID, TITLE, TEXT, METRICS, USER_ID, LIKES_COUNT, DISLIKES_COUNT, IS_HIDDEN, CREATION_DATE
	FROM DBO.TOPICS
	ORDER BY CREATION_DATE DESC
END
GO

CREATE PROCEDURE [DBO].[QUERY_COMMENT_BY_ID]
	@ID INT
AS
BEGIN
	SELECT ID, TEXT, PARENT_TYPE_ID, PARENT_ID, USER_ID, LIKES_COUNT, DISLIKES_COUNT, IS_HIDDEN, CREATION_DATE
	FROM DBO.COMMENTS
	WHERE ID = @ID
END
GO

CREATE PROCEDURE [DBO].[QUERY_NEWS_BY_ID]
	@ID INT
AS
BEGIN
	SELECT ID, TITLE, TEXT, METRICS, USER_ID, LIKES_COUNT, DISLIKES_COUNT, IS_HIDDEN, CREATION_DATE
	FROM DBO.NEWS
	WHERE ID = @ID
END
GO

CREATE PROCEDURE [DBO].[QUERY_TOPIC_BY_ID]
	@ID INT
AS
BEGIN
	SELECT ID, TITLE, TEXT, METRICS, USER_ID, LIKES_COUNT, DISLIKES_COUNT, IS_HIDDEN, CREATION_DATE
	FROM DBO.TOPICS
	WHERE ID = @ID
END
GO

CREATE PROCEDURE [DBO].[UPDATE_COMMENT]
	@ID INT,
	@TEXT TEXT,
	@PARENT_TYPE_ID INT,
	@PARENT_ID INT,
	@USER_ID INT,
	@LIKES_COUNT INT,
	@DISLIKES_COUNT INT,
	@IS_HIDDEN BIT
AS
BEGIN
	UPDATE DBO.COMMENTS
	SET
		TEXT = @TEXT,
		PARENT_TYPE_ID = @PARENT_TYPE_ID,
		PARENT_ID = @PARENT_ID,
		USER_ID = @USER_ID,
		LIKES_COUNT = @LIKES_COUNT,
		DISLIKES_COUNT = @DISLIKES_COUNT,
		IS_HIDDEN = @IS_HIDDEN
	WHERE ID = @ID
END
GO

CREATE PROCEDURE [DBO].[UPDATE_NEWS]
	@ID INT,
	@TITLE VARCHAR(200),
	@TEXT TEXT,
	@METRICS VARCHAR(50),
	@USER_ID INT,
	@LIKES_COUNT INT,
	@DISLIKES_COUNT INT,
	@IS_HIDDEN BIT
AS
BEGIN
	UPDATE DBO.NEWS
	SET
		TITLE = @TITLE,
		TEXT = @TEXT,
		METRICS = @METRICS,
		USER_ID = @USER_ID,
		LIKES_COUNT = @LIKES_COUNT,
		DISLIKES_COUNT = @DISLIKES_COUNT,
		IS_HIDDEN = @IS_HIDDEN
	WHERE ID = @ID
END
GO

CREATE PROCEDURE [DBO].[UPDATE_TOPIC]
	@ID INT,
	@TITLE VARCHAR(200),
	@TEXT TEXT,
	@METRICS VARCHAR(50),
	@USER_ID INT,
	@LIKES_COUNT INT,
	@DISLIKES_COUNT INT,
	@IS_HIDDEN BIT
AS
BEGIN
	UPDATE DBO.TOPICS
	SET
		TITLE = @TITLE,
		TEXT = @TEXT,
		METRICS = @METRICS,
		USER_ID = @USER_ID,
		LIKES_COUNT = @LIKES_COUNT,
		DISLIKES_COUNT = @DISLIKES_COUNT,
		IS_HIDDEN = @IS_HIDDEN
	WHERE ID = @ID
END
GO
