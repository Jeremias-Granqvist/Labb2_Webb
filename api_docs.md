# My API

**Version:** V1  
**Description:** An API to make calls needed for this climbing store it's users and orders  
**Contact:** Jeremias - Jeremias.Granqvist@iths.se  
**License:** [MIT License](https://opensource.org/licenses/MIT)

---

## Endpoints

### `POST /api/Account/register`
**Summary:** Register a new user  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| body | body | object | The registration data for the new user, including first name, last name, email, etc. |

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

---
### `POST /api/Account/login`
**Summary:** Logs user in to website  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| body | body | object | The login data for the user, including username/email and password. |

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

---
### `POST /api/Account/logout`
**Summary:** Non-functioning at the moment  
**Description:**   
**Parameters:** None

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

---
### `POST /api/Account/refresh-token`
**Summary:** Refreshes a users JWT if they're logged in.  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| body | body | object | The user's session information, which is used to validate and refresh the JWT token. |

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

---
### `GET /api/Adress`
**Summary:** returns a list of adresses from DB  
**Description:**   
**Parameters:** None

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

---
### `POST /api/Adress`
**Summary:** Adds mew adress to database  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| body | body | object | The address data to be added to the database, including street name, city, zip code, and country. |

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

---
### `GET /api/Adress/{id}`
**Summary:** Get specific adress based on AdressId  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| id | path | integer | The unique identifier for the address to retrieve. |

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

---
### `PUT /api/Adress/{id}`
**Summary:** update adress already in database  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| id | path | integer | The unique identifier for the address that needs to be updated. |
| body | body | object | The updated address data, including street name, city, zip code, and country. |

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

---
### `GET /api/Category`
**Summary:** Get a list of all categories  
**Description:**   
**Parameters:** None

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

---
### `GET /api/Customer`
**Summary:** Get a list of all customers  
**Description:**   
**Parameters:** None

**Responses:**
| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |


**Example**
```json
  {
    "userId": 2,
    "firstName": "test",
    "lastName": "testson",
    "email": "hej@hej.se",
    "password": "$2a$11$NTbQzQGJ1zELybRZrAvlOutpmnsSfjh0Q5T46cyfot.UG2VzXpAbi",
    "role": "User",
    "phoneNumber": "111222333444",
    "addressId": 2,
    "adress": null
  }, 
```
---
### `POST /api/Customer`
**Summary:** Create new user and save to DB.  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| body | body | object | The `ApplicationUser` object containing the details of the user to be created, such as name, email, etc. |

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

---
### `GET /api/Customer/{id}`
**Summary:** Get specific user from userID  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| id | path | integer | The ID of the user to retrieve from the database. |

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

---
### `PUT /api/Customer/{id}`
**Summary:** updates customer information in database  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| id | path | integer | The ID of the user to update. |
| body | body | object | The `ApplicationUser` object containing the updated user information. |

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

---
### `GET /api/Order`
**Summary:** Get a list of all orders  
**Description:**   
**Parameters:** None

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

---
### `GET /api/Order/{id}`
**Summary:** sends information about order from OrderID  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| id | path | integer | The ID of the order to retrieve. This ID is used to fetch the order details from the database, including customer and product information. |

**Responses:**
| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

**Example**
```json
{
  "orderId": 2,
  "userID": 1,
  "dateOfOrder": "2025-03-08",
  "user": {
    "userId": 1,
    "firstName": "Admin",
    "lastName": "Adminson",
    "email": "Admin@Admin.Admin",
    "password": "$2a$11$J3E0d6A.ux7tfwUQysUJOu7LoYQp40nvV2RRFlur2goL45V3VroD6",
    "role": "Admin",
    "phoneNumber": "111222333444",
    "addressId": 2,
    "adress": null
  },
  "products": [
    {
      "id": 1,
      "name": "Edelrid Boa 60m",
      "description": "60m dynamic climbing rope",
      "price": 1400,
      "categoryId": 1,
      "status": false
    }
  ]
}
```

---
### `POST /api/Order/place-order`
**Summary:**   
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| body | body | object |  |

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

---
### `GET /api/Product`
**Summary:** returns a list of all products  
**Description:**   
**Parameters:** None

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

---
### `POST /create-product`
**Summary:** Adds new product to database.  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| body | body | object | The product object to be added to the database. This contains product details such as name, price, description, etc. |

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

**Example** 

input data: 
{
  "name": "testproduct",
  "description": "this is the description of the testproduct",
  "price": 100,
  "categoryId": 1,
  "status": true
}

|Returns | 200|OK |

---
### `PUT /api/Product/{id}`
**Summary:**   
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| id | path | integer |  |
| body | body | object |  |

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

---
### `DELETE /api/Product/{id}`
**Summary:** deletes product with specified ID  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| id | path | integer | The ID of the product to be deleted. This ID is used to find and remove the product from the database. |

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |

---
### `GET /api/categories`
**Summary:** testendpoint currently fetches all Categories to a list  
**Description:**   
**Parameters:** None

**Responses:**

| Code | Description | example response |
|------|-------------|-----------------------------------------------------|
| 200 | OK |
| 404 | Not Found | The resource was not found. |
| 400 | Bad Request | Invalid parameters or missing required fields. |
| 500 | Internal Server Error | If something goes wrong on the server side. |
---

### Error codes and responses
404: Not Found - The resource was not found.
400: Bad Request - Invalid parameters or missing required fields.
500: Internal Server Error - If something goes wrong on the server side.

