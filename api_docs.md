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

| Code | Description |
|------|-------------|
| 200 | OK |

---
### `POST /api/Account/login`
**Summary:** Logs user in to website  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| body | body | object | The login data for the user, including username/email and password. |

**Responses:**

| Code | Description |
|------|-------------|
| 200 | OK |

---
### `POST /api/Account/logout`
**Summary:** Non-functioning at the moment  
**Description:**   
**Parameters:** None

**Responses:**

| Code | Description |
|------|-------------|
| 200 | OK |

---
### `POST /api/Account/refresh-token`
**Summary:** Refreshes a users JWT if they're logged in.  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| body | body | object | The user's session information, which is used to validate and refresh the JWT token. |

**Responses:**

| Code | Description |
|------|-------------|
| 200 | OK |

---
### `GET /api/Adress`
**Summary:** returns a list of adresses from DB  
**Description:**   
**Parameters:** None

**Responses:**

| Code | Description |
|------|-------------|
| 200 | OK |

---
### `POST /api/Adress`
**Summary:** Adds mew adress to database  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| body | body | object | The address data to be added to the database, including street name, city, zip code, and country. |

**Responses:**

| Code | Description |
|------|-------------|
| 200 | OK |

---
### `GET /api/Adress/{id}`
**Summary:** Get specific adress based on AdressId  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| id | path | integer | The unique identifier for the address to retrieve. |

**Responses:**

| Code | Description |
|------|-------------|
| 200 | OK |

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

| Code | Description |
|------|-------------|
| 200 | OK |

---
### `GET /api/Category`
**Summary:** Get a list of all categories  
**Description:**   
**Parameters:** None

**Responses:**

| Code | Description |
|------|-------------|
| 200 | OK |

---
### `GET /api/Customer`
**Summary:** Get a list of all customers  
**Description:**   
**Parameters:** None

**Responses:**

| Code | Description |
|------|-------------|
| 200 | OK |

---
### `POST /api/Customer`
**Summary:** Create new user and save to DB.  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| body | body | object | The `ApplicationUser` object containing the details of the user to be created, such as name, email, etc. |

**Responses:**

| Code | Description |
|------|-------------|
| 200 | OK |

---
### `GET /api/Customer/{id}`
**Summary:** Get specific user from userID  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| id | path | integer | The ID of the user to retrieve from the database. |

**Responses:**

| Code | Description |
|------|-------------|
| 200 | OK |

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

| Code | Description |
|------|-------------|
| 200 | OK |

---
### `GET /api/Order`
**Summary:** Get a list of all orders  
**Description:**   
**Parameters:** None

**Responses:**

| Code | Description |
|------|-------------|
| 200 | OK |

---
### `GET /api/Order/{id}`
**Summary:** sends information about order from OrderID  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| id | path | integer | The ID of the order to retrieve. This ID is used to fetch the order details from the database, including customer and product information. |

**Responses:**

| Code | Description |
|------|-------------|
| 200 | OK |

---
### `POST /api/Order/place-order`
**Summary:**   
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| body | body | object |  |

**Responses:**

| Code | Description |
|------|-------------|
| 200 | OK |

---
### `GET /api/Product`
**Summary:** returns a list of all products  
**Description:**   
**Parameters:** None

**Responses:**

| Code | Description |
|------|-------------|
| 200 | OK |

---
### `POST /create-product`
**Summary:** Adds new product to database.  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| body | body | object | The product object to be added to the database. This contains product details such as name, price, description, etc. |

**Responses:**

| Code | Description |
|------|-------------|
| 200 | OK |

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

| Code | Description |
|------|-------------|
| 200 | OK |

---
### `DELETE /api/Product/{id}`
**Summary:** deletes product with specified ID  
**Description:**   
**Parameters:**

| Name | In | Type | Description |
|------|----|------|-------------|
| id | path | integer | The ID of the product to be deleted. This ID is used to find and remove the product from the database. |

**Responses:**

| Code | Description |
|------|-------------|
| 200 | OK |

---
### `GET /api/categories`
**Summary:** testendpoint currently fetches all Categories to a list  
**Description:**   
**Parameters:** None

**Responses:**

| Code | Description |
|------|-------------|
| 200 | OK |

---