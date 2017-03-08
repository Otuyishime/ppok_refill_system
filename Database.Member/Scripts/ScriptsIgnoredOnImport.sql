
/*	Initial data for template types	*/
INSERT INTO TemplateType VALUES ('REFILL', 'This type is for refills');
GO

INSERT INTO TemplateType VALUES ('RECALL', 'This type is for recalls');
GO

INSERT INTO TemplateType VALUES ('BIRTHDAY', 'This type is for birthday');
GO

INSERT INTO TemplateType VALUES ('REFILL_CONFIRMED', 'This type is for confirmation of refill');
GO

select * from TemplateType
GO

/*	Initial data for communication type	*/
INSERT INTO CommunicationType VALUES('TEXT', 'This type is for text');
GO

INSERT INTO CommunicationType VALUES('VOICE', 'This type is for voice');
GO

INSERT INTO CommunicationType VALUES('EMAIL', 'This type is for email');
GO

SELECT * FROM CommunicationType
GO

/*	Initial data for template message	*/
INSERT INTO Template VALUES (1, 1, 'Hi - this is a text message. Answer yes to be okay. Answer no to see me.');
GO

/*	TEXT - REFILL */
INSERT INTO Template VALUES (1, 2, 'Hi - this is a text message. We are recalling you medication.');
GO

/*	TEXT - RECALL */
INSERT INTO Template VALUES (1, 3, 'Hi - this is a text message. Happy birthday.');
GO

/*	TEXT - BIRTHDAY */
INSERT INTO Template VALUES (1, 4, 'Hi - this is a text message. You refill has been done.');
GO

/*	TEXT - REFILL_CONFIRMED */

INSERT INTO Template VALUES (2, 1, 'Hi - this is a voice message. Press one to be okay. Press no to see me.');
GO

/*	VOICE - REFILL */
INSERT INTO Template VALUES (2, 2, 'Hi - this is a voice message. We are recalling you medication.');
GO

/*	VOICE - RECALL */
INSERT INTO Template VALUES (2, 3, 'Hi - this is a voice message. Happy birthday.');
GO

/*	VOICE - BIRTH */
INSERT INTO Template VALUES (2, 4, 'Hi - this is a voice message. You refill has been done.');
GO

/*	VOICE - REFILL_CONFIRMED */

INSERT INTO Template VALUES (3, 1, 'Hi - this is an email. We have no options.');
GO

/*	EMAIL - REFILL */
INSERT INTO Template VALUES (3, 2, 'Hi - this is an email. We are recalling you medication.');
GO

/*	EMAIL - RECALL */
INSERT INTO Template VALUES (3, 3, 'Hi - this is an email. Happy birthday.');
GO

/*	EMAIL - BIRTH */
INSERT INTO Template VALUES (3, 4, 'Hi - this is an email. You refill has been done.');			/*	EMAIL - REFILL_CONFIRMED */
GO
