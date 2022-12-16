# DisprzTraining
Calendar Project


TEST CASES:

GetAll - 
	1. Success
	2. Count

GetByID - 
	1.Success
	2. Return Items
	3. 404 -NotFound
	4. Bad Request - Empty ID.

GetByName - 
	1. Success
	2. Return Items
	3. 404 - NotFound
	4. Bad Request - Empty string
	5. BL Returns NullReference if Get by empty string.

POST - 
	1. Success - Check By Name
	2. Success code - 201
	3. Conflict - 409
	4. BadRequest - Null
	5. BL Returns NullReference if post with null value.

PUT - 
	1. Success
	2. BadRequest
	3. 409 - conflict updating meeting with existing time.
	4. BL Returns NullReference if updating with null value.

DELETE By ID-
	1. Success - GetAllItemsCount
	2. NoContent
	3. 404 - Empty string
	4. 404 - WrongID
	5. BL Returns InvalidOperationException
