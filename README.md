# PROJECT WEBSITE PROGRAMING - ASP.NET MVC CORE - 2022 - BINHDINHFOOD

## Abstract

- E-commerce website manage shop, sell many item about food and traditional food. The system has admin area for add, update, delete product, category, banner, ... and user area to buy product, comment, search, add in cart, ...
- Website was built by **ASP.NET CORE** the open source technology, apply **repository pattern** to manage and maintain source in the future.
- Here is short demo about `BinhDinhFood` website. [Click here to see full!!!](https://youtu.be/Zy37v0df-mM)

<video src="assets/quick demo.mp4"></video>

# Idea

### Objects use

- User: Buy product, wish list, cart CRUD, comment & rating, filter, edit profile, register, login, forgot password, search, filter, .
- Admin:
  - Dashboard: tracking activity, number product sell, customer register.
  - Management: product, category, banner, .
  - Statistic: revenue month, revenue year, user subscribe month, user subscribe year, number of subscribers.

### Class diagram

![image-20221010023625598](assets/image-20221010023625598.png)

### Overview Use case

![image-20221010023637928](assets/image-20221010023637928.png)

## Requirements

- C#
- Entity Framework (code first)
- Asp.net MCV Core
- Other library: pagelist, md5, mail, jquery, bootstrap, ...
- Database (MSSQL)
- API VNPAY
- Mail model
- MD5 encrypt
- signal Rm Html, css, js, Jquery

### Diagram

<img src="./assets/image-20220922105818404.png" alt="image-20220922105818404" style="zoom:70%;" />

#### Interface and Function of website

##### Home page

##### Login & Register & Reset Password

| Login                                                        | Register                                                     | Reset password                                               |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| ![image-20221010023809288](assets/image-20221010023809288.png) | ![image-20221010023803876](assets/image-20221010023803876.png) | ![image-20221010023814363](assets/image-20221010023814363.png) |

##### About us & Page not found

| About us                                                     | Contact                                                      | 404 pages                                                 | Help                                                         |
| ------------------------------------------------------------ | ------------------------------------------------------------ | --------------------------------------------------------- | ------------------------------------------------------------ |
| <img src="assets/image-20221010023954882.png" alt="image-20221010023954882"  /> | ![image-20221010024001701](assets/image-20221010024001701.png) | <img src="assets/404.png" alt="404" style="zoom: 25%;" /> | ![image-20221010024506243](assets/image-20221010024506243.png) |

#### User

| USER PROFILE                                                 | EDIT PROFILE                                                 | CHANGE PASS                                                  |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| ![image-20221010023856181](assets/image-20221010023856181.png) | ![image-20221010023904676](assets/image-20221010023904676.png) | ![image-20221010023928484](assets/image-20221010023928484.png) |

##### Admin

| Login                                                        | Admin forget password                                        |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| ![image-20221010024629345](assets/image-20221010024629345.png) | ![image-20221010024657796](assets/image-20221010024657796.png) |

##### Home Page Admin

![image-20221010024710684](assets/image-20221010024710684.png)

##### Admin management (C-R-U-D)

| Product                                                      | Blog                                                         | Order                                                        | Customer                                                     |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| ![image-20221010025123262](assets/image-20221010025123262.png) | ![image-20221010025144814](assets/image-20221010025144814.png) | ![image-20221010025153528](assets/image-20221010025153528.png) | ![image-20221010025247921](assets/image-20221010025247921.png) |

##### example: Product Management

| List film                                                    | Edit                                                         | Detail                                                       | Delete                                                       |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| ![image-20221010024827359](assets/image-20221010024827359.png) | ![image-20221010024836190](assets/image-20221010024836190.png) | ![image-20221010024913510](assets/image-20221010024913510.png) | ![image-20221010024923660](assets/image-20221010024923660.png) |

#### Statical and analyst

| Analyst Type of product sell in year                         | Product analyst (line chart)                                 | Product analyst (bar chart)                                  | Product sell in month                                        |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| ![image-20221010025012953](assets/image-20221010025012953.png) | ![image-20221010025017927](assets/image-20221010025017927.png) | ![image-20221010025021862](assets/image-20221010025021862.png) | ![image-20221010025025773](assets/image-20221010025025773.png) |

Above is all of function we made

Any question you can contact with us

email: <vothuongtruongnhon2002@gmail.com>

Author:

| leader                | member       | member         |
|-----------------------|--------------|----------------|
| Võ Thương Trường Nhơn | Phạm Đức Tài | Phạm Hồng Thái |

**## 👊 Ủng hộ Binhdinhfood**

\- Bằng cách ⭐️ repo này nhé! ❤️
