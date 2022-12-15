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

POST - 
	1. Success - Check By Name
	2. Success code - 201
	3. Conflict - 409
	4. BadRequest - Null

PUT - 
	1. Success
	2. BadRequest

DELETE By ID-
	1. Success
	2. NoContent
	3. GetAllItemsCount
	4. 404 - Empty string
	5. 404 - WrongID
