using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BinhDinhFood.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdminPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdminEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminDateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    BannerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BannerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductDiscount = table.Column<int>(type: "int", nullable: true),
                    BannerPrice = table.Column<double>(type: "float", nullable: true),
                    BannerDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BannerImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BannerDateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.BannerId);
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlogImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlogDateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.BlogId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryDateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerFullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerDateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerState = table.Column<bool>(type: "bit", nullable: false),
                    CustomerImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    TokenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expiry = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.TokenID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductPrice = table.Column<double>(type: "float", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductAmount = table.Column<int>(type: "int", nullable: false),
                    ProductDiscount = table.Column<int>(type: "int", nullable: false),
                    ProductRating = table.Column<int>(type: "int", nullable: false),
                    ProductImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductDateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayDelivery = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidState = table.Column<bool>(type: "bit", nullable: false),
                    DeliveryState = table.Column<bool>(type: "bit", nullable: false),
                    TotalMoney = table.Column<double>(type: "float", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorite",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    PRDateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorite", x => new { x.ProductId, x.CustomerId });
                    table.ForeignKey(
                        name: "FK_Favorite_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorite_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductRating",
                columns: table => new
                {
                    ProductRatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    RatingContent = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PRDateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRating", x => x.ProductRatingId);
                    table.ForeignKey(
                        name: "FK_ProductRating_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRating_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => new { x.ProductId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Banner",
                columns: new[] { "BannerId", "BannerDateCreated", "BannerDescription", "BannerImage", "BannerName", "BannerPrice", "ProductDiscount" },
                values: new object[,]
                {
                    { 6, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "banner1sss", "slide_home_1.jpg", "Chả cá Quy Nhơn", 100000.0, 0 },
                    { 8, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "banner2", "slide_home_2.jpg", "Gỏi cá Chình", 200000.0, 0 },
                    { 9, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "banner3", "slide_home_3.jpg", "Nem chợ huyện", 150000.0, 0 },
                    { 10, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "banner4", "slide_home_4.jpg", "Nem chợ huyện", 150000.0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Blog",
                columns: new[] { "BlogId", "BlogContent", "BlogDateCreated", "BlogImage", "BlogName" },
                values: new object[,]
                {
                    { 1, "Mực rim là món ăn phổ biến khiến du thích yêu thích có mặt ở hầu hết những vùng biển lớn. Tuy nhiên mực rim hay còn gọi là mực ngào Bình Định có một hương vị thơm ngon rất riêng từ vùng biển duyên hải miền Trung. Mực rim Quy Nhơn được người dân làm từ những con mực tươi nhất và hương vị không nơi nào giống được. Với hương thơm ngon đặc biệt cùng vị cay cay kích thích vị giác, mực rim trở thành món ăn vặt siêu ngon và được mọi người vô cùng yêu thích. Đồng thời, với những hũ mực rim được làm sẵn giúp bạn có thể dễ dàng lựa chọn đặc sản Bình Định này để làm quà tặng.", new DateTime(2022, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "mucrim.png", "Mực rim Quy Nhơn" },
                    { 2, "Với tên gọi độc và lạ của món Chả Tré, món đặc sản Bình Định này đã gợi nên sự tò mò với nhiều du khách muốn tìm hiểu và được thử món ăn độc đáo này. Mặc dù đã có mặt phổ biến khắp các tỉnh thành Trung Trung bộ, nhưng hương vị thơm ngon nhất vẫn là chả Tré Bình Định với cách làm và công thức chỉ vùng đất Bình Định mới làm nên được.Thành phần nguyên liệu làm chả Tré cũng tương tự với các loại nem, bì của miền bắc. Nhưng Tré Bình Định được người dân nơi đây khéo léo thay thế bằng nhiều loại nguyên liệu khác như tai heo, lỗ mũi heo, da heo, thịt ba chỉ,...Tré cũng thích hợp để trở thành món quà mang về khi bạn đến thăm Bình Định.", new DateTime(2022, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "chatre.png", "Chả Tré rơm" },
                    { 3, "Mặc dù chả cá có thể là đặc sản và có mặt ở nhiều nơi nhưng không phải hương vị chả cá nào cũng như nhau. Sở dĩ chả cá Quy Nhơn là một trong các đặc sản Bình Định nổi tiếng vì vị ngon và lạ đặc trưng. Với nguyên liệu được tuyển chọn từ những con cá biển tươi ngon nhất và công thức chế biến độc quyền của người dân đã tạo nên sự khác biệt cho chả cá Quy Nhơn. Chả cá Quy Nhơn phổ biến có 2 loại là chả hấp và chả chiên. Ngoài việc thưởng thức thực tiếp miếng chả dai, giòn, thơm ngon đặc biệt, các bạn có thể dùng chả cá này để làm “topping” cho các món ăn khác như cơm, bún, phở. Đây cũng là một lựa chọn thích hợp để bạn mua về làm quà cho người thân và bạn bè nữa đó.", new DateTime(2022, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "chaca.png", "Chả cá Quy Nhơn" },
                    { 4, "Đây là một món ngon đặc sản Quy Nhơn rất đỗi bình dị nhưng được du khách rất yêu thích. Nó được bày bán ở hầu hết các quán xá vỉa hè ở Bình Định. Bánh xèo được làm được những  nguyên liệu đơn giản như thịt heo băm nhuyễn, hành phi, rau thơm, trứng và bột gạo. Gaọ sẽ được tuyển chọn những những gạo to chắc mẩy không bị sâu để tạo độ ngọt của bánh. Gạo sẽ được đem đi xay và nấu bột thành một thứ hỗn hợp dẻo, đập trứng cho thịt băm và một số loại gia vị vào. Bên cạnh đó đac có một cái chảo đang được đun nóng. Người nấu sẽ múc từng múc lên chảo để tráng những miếng bánh, dải thịt băm nhuyễn đã được xào chín lên bên trên bề mặt bánh và guộn đều tay để bánh to tròn và đẹp. Hoặc có thể là những con tôm tươi ngon. Khi  ăn ăn kèm với rau thơm và nước chấm.", new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "banh-xeo-my-cang-dac-san-binh-dinh.vntrip-e1501650332846.jpg", "Bánh Xèo Mỹ Cang" },
                    { 5, "Khi đến du lịch Bình Định không thể không nhắc tới  món bánh tráng nước dừa. Đây là một món đặc sản nơi xứ dừa. Công đoạn chế biến bánh không quá cầu kì nhưng đòi hỏi sự tỉ mỉ, có kinh nghiệm của người tráng bánh. Nguyên liệu của bánh chủ yếu là Củ Mì ( củ sắn) được sắt nhỏ, xay lấy nước. Cơm dừa được nạo thành sợi nhỏ,  nước dừa và vừng đen. Tất cả đều được đổ chung vào một nồi lớn, trộn đều cho các gia vị hòa quyện cùng với nhau và được đun nóng. Bên cạnh đó có một chảo đang được đun nóng. Khi chảo nóng lên người tráng bánh sẽ dùng một cái gáo làm bằng sọ dừa có cán dài múc từng gáo nước bánh lên chảo và tráng đều. Tráng bánh phải đều tay để cơm dừa và vừng đen được dàn đều mặt bánh. Bánh phải tròn mỏng và không bị chỗ dày, chỗ mỏng thì mới là bánh đạt chuẩn. cứ tráng được mười chiếc  bánh thì đem ra phơi. khi ăn bạn cần nướng lên để bánh có độ phồng và dậy hết mùi thơm của vừng, của nước cốt dừa và cơm dừa. Có thể ăn bánh thay cơm ăn chỉ thấy no mà không thấy chán.", new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Banh-trang-nuoc-dua-am-thuc-binh-dinh.jpg", "Bánh tráng nước dừa" },
                    { 6, "Bún song thần có chút khác biệt với các loại bún thông thường khác bởi thay vì sợi bún được làm từ bột gạo hay bột củ mì kéo sợi thì bún song thần lại được làm từ bột đậu xanh. Bún Song thần đặc sản Bình Định có màu trắng đặc trưng. Bún được đặt song  song bên nhau nên có tên là bún song thần. Món đặc sản này có giá trị dinh dưỡng cao hơn các loại bún khác. Tuy nhiên giá thành của bún khá cao bởi 5kg đậu xanh chỉ là được 1kg bún.", new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "bat-bun-song-than.jpg", "Bún song  thần" },
                    { 7, "Món món hải sản ngon nức tiếng ở Bình Định. Cua Huỳnh đếđược xem là vua của các loại cua bởi nó có mai đỏ vàng như một bộ long bào uy nghi của các nhà vua, hai bên có gai li ti sắc nhọn, hai chiếc càng to chắc khỏe. Cua thường sống trong những ngách đá trên biển Bình Định. Cua Huỳnh Đế có thịt thơm, chắc  và có thể chế biến thành nhiều món ăn ngon khác nhau như cua nướng, cua hấp… đều rất thơm ngon.", new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "cua-huynh-de-vntrip-e1536313616403.jpg", "Cua Huỳnh Đế" },
                    { 8, "Cá Chích là loại cá nước ngọt sống ở các sông hồ ao suối. Cũng bởi Bình Định có nhiều sông hồ nên đây là môi trường thuận lợi để loài đặc sản này sinh sống. Cá Chích đặc sản Bình Định có thân  hình nhỏ, dài. Cá Chích sau khi được  đánh bắt lên sẽ được làm  sạch  và chiên giòn. Vì  là loài cá có kích cỡ nhỏ nên  khi ăn  người ta sẽ kẹp cả con cá đã được chiên vàng ăn với bánh phở cuốn, kèm rau thơm, dưa chuột. Cá ngọt thịt nên bạn ăn sẽ không bị chán. Tuy Nhiên nếu bạn là tín đồ gỏi sống bạn có thể được  thưởng thức gỏi cá chích với những thớ thịt  đc lọc xương làm sạch.", new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "goi-ca-chinh-binh-dinh.jpg", "Gỏi cá chích" },
                    { 9, "Bánh hồng Tam Quan là một trong những món đặc sản của Bình Định, được xem như biểu trưng cho tin vui, thường xuất hiện trong các dịp cưới hỏi của người dân nơi đây. Điều đặc biệt bánh hồng Tam Quan là bánh được làm từ gạo nếp Ngự nổi tiếng dẻo thơm. Do hoàn toàn không có chất bảo quản nên bánh chỉ để được 5 ngày thôi bạn nhé.", new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "banhhong.jpg", "Bánh hồng Tam Quan" },
                    { 10, "Bánh tráng chả cá là một trong những đặc sản nổi tiếng gần xa của Bình Định. Bánh tráng chả cá được làm từ nguyên liệu chính là cá cùng một ít gia vị và bột năng. Để món ăn đúng vị hơn bạn nên ăn kèm với rau răm nhé.", new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "banhtrangchaca.jpg", "Bánh tráng chả cá" },
                    { 11, "Nếu đã đến với đất Bình Định thì bạn nhất định phải thử qua món mực ngào vị tỏi nhé. Món ăn là sự hòa quyện giữa vị mực vừa tươi vừa giòn cùng vị cay đặc trưng của ớt và tỏi. Bạn nhớ bảo quản món này ở nhiệt độ thoáng mát nha.", new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mực ngào vị tỏi", "Mực ngào vị tỏi" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryDateCreated", "CategoryName" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đồ khô" },
                    { 6, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bánh truyền thống" },
                    { 7, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đồ đặc sản" }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerDateCreated", "CustomerEmail", "CustomerFullName", "CustomerImage", "CustomerPassword", "CustomerPhone", "CustomerState", "CustomerUserName" },
                values: new object[,]
                {
                    { 1, "48/29/8 Lê Văn Hưng, Quy Nhơn", new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "vothuongtruongnhon2002@gmail.com", "Võ Thương Trường Nhơn", "nhon.jpg", "$2a$11$wyASfN6dgcEIvKJTH9OULugQhQaDG4wh8CT5H1vYbukCpMel93Bje", "0905726748", true, "truongnhon" },
                    { 2, "Tây Ninh", new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "phamhongthai@gmail.com", "Nguyễn Hồng Thái", "thai.jpg", "$2a$11$N83LOLEM6I2i/Vz1JXkZBesfRa3Wbom3HIIqilAkOfD0WB0BMFvPC", "0905726748", true, "thai" },
                    { 3, "Nam Định", new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "taiphamduc@gmail.com", "Phạm Đức Tài", "tai.jpg", "$2a$11$xA3JoltvDGn3LkZGstK.DuWlBS0dvHkEJe.4DZcPkoA2lw4kEr2NK", "0905726748", true, "tai" },
                    { 4, "Saigon", new DateTime(2022, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "vothuongtruongnhon2002@gmail.com", "dotnet evil", "0d45bade8d1e402fa2615717f3808101.jpg", "$2a$11$QoGGyzIb5jTvOj.H/nrBbep4BW.Ijr8XrOZoXI5f1T4Q2QPx2Wtty", "0905726748", true, "nhondeptrai" },
                    { 16, "America", new DateTime(2022, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "vothuongtruongnhon2002@gmail.com", "Jeff Bezos", "avatar.jpg", "$2a$11$J8RrRahw.3NDglMQ9RkQ/.8YhKNnnMliILWyjI1FbCS5a2fvYD2RO", "0905726748", true, "acc1" },
                    { 18, "NewYork", new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "vothuongtruongnhon2002@gmail.com", "Bill Gate", "avatar.jpg", "$2a$11$SXgawt7Bt.4HoXCSdZfjTOCGozYLxO6PSBdR21Sfhdvx0nCVKJ.Va", "0905726748", true, "acc2" },
                    { 19, "NewWorld", new DateTime(2022, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "vothuongtruongnhon2002@gmail.com", "Edward NewGate", "avatar.jpg", "$2a$11$DkFSgACkVr2ZmnmzI1EK2.TTKJVA5yPTMqXJaRoFAoPNnoV/ff4SO", "0905726748", true, "acc3" },
                    { 22, "East Sea", new DateTime(2022, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "vothuongtruongnhon2002@gmail.com", "Monkey D Luffy", "avatar.jpg", "$2a$11$/uTIpIg.9evkUVPcmrgJUuNuJpgmxQ6l34IEEonZfIA.1yR0tbqTK", "0905726748", true, "acc4" },
                    { 23, "Bình Định", new DateTime(2022, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "vothuongtruongnhon2002@gmail.com", "Ái Quyên President", "avatar.jpg", "$2a$11$pl8OaNWWLv5cRAm2DIMZyuJEziB/UAnDivRC7HGs7ILH4rgWkz3pW", "0905726748", true, "acc5" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "CategoryId", "ProductAmount", "ProductDateCreated", "ProductDescription", "ProductDiscount", "ProductImage", "ProductName", "ProductPrice", "ProductRating" },
                values: new object[,]
                {
                    { 3, 1, 100, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mặc dù chả cá có thể là đặc sản và có mặt ở nhiều nơi nhưng không phải hương vị chả cá nào cũng như nhau. Sở dĩ chả cá Quy Nhơn là một trong các đặc sản Bình Định nổi tiếng vì vị ngon và lạ đặc trưng. Với nguyên liệu được tuyển chọn từ những con cá biển tươi ngon nhất và công thức chế biến độc quyền của người dân đã tạo nên sự khác biệt cho chả cá Quy Nhơn.\n Chả cá Quy Nhơn phổ biến có 2 loại là chả hấp và chả chiên. Ngoài việc thưởng thức thực tiếp miếng chả dai, giòn, thơm ngon đặc biệt, các bạn có thể dùng chả cá này để làm “topping” cho các món ăn khác như cơm, bún, phở. Đây cũng là một lựa chọn thích hợp để bạn mua về làm quà cho người thân và bạn bè nữa đó.", 10, "chaca.png", "Chả cá Quy Nhơn", 120000.0, 0 },
                    { 6, 1, 20, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tôm khô còn gọi là tôm nõn khô là một trong các loại thực phẩm giàu dinh dưỡng rất tốt cho sức khỏe. Chúng được làm từ tôm tươi tự nhiên và phơi khô dưới ánh nắng mặt trời hoặc sấy khô thủ công. 1kg tôm tươi làm được khoảng 2 lạng tôm khô, thành phẩm tôm có kích thước nhỏ hơn, có vị ngọt thanh đậm đà rất thơm.\nGiá trị dinh dưỡng của tôm vẫn giữ gần như nguyên vẹn, trong 100g tôm khô có: 347 kcal, 75,6g đạm, 235mg canxi, 4,6mg sắt, vitamin B1, B2, PP và 3,8g chất béo chưa bảo hòa.", 20, "tom-kho-gia-bao-nhieu-1kg.jpg", "Tôm khô", 84000.0, 0 },
                    { 7, 7, 100, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nhum có rất nhiều loại khác nhau, nhưng mắm nhum tại Bình Định đặc biệt được làm từ con nhum ta, tạo hương vị ngon đến nỗi “ăn với món gì cũng ngon”. Đồng thời mắm Nhum tại Mỹ An cũng từng là đặc sản Bình Định được tiến vua, và hiện nay là một món ăn mà du khách không thể bỏ lỡ khi đến Bình Định du lịch.\nNhum vốn là động vật với bê ngoài gai góc có thể làm đau người dân nếu đạp phải, và người dân nơi đây đã biến chúng thành một món ngon tuyệt vời. Mắm nhum còn có thể là món quà hảo hạng giúp bạn dùng làm quà tặng sau khi đến Bình Định du lịch, nếu được thì bạn nên đến Mỹ An để mua mắm nhum nhé.", 5, "mamnhum.png", "Mắm Nhum Mỹ An Bình Định", 20000.0, 0 },
                    { 16, 1, 55, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "cá cơm giàu vitamin A, nhiều axit béo, vitamin E, canxi, Vitamin A, giúp mắt sáng, ngăn ngừa các bệnh về mắt, duy trì làn da khỏe mạnh. Ăn cá cơm giúp giảm lượng cholesterol trong máu, ngăn ngừa các bệnh về tim mạch.\nCá cơm cung cấp một lượng lớn protein và đạm, nên chúng được sử dụng để làm nước mắm nhĩ", 15, "cach-lam-ca-kho-tam-gia-vi.jpg", "Cá cơm khô", 18000.0, 0 },
                    { 17, 6, 100, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nem chua là một trong các đặc sản Bình Định được chế biến cầu kỳ và công phu. Với công thức hương vị đặc biệt để ướp những miếng thịt heo tươi ngon nhất và gói bên trong những lớp lá khế non và lớp lá chuối cầu kì, hương vị thơm ngon nổi tiếng của nem chợ huyện cũng từ đó mà vang xa.\nĐến Bình Định ngồi cắn một miếng nem và nhâm nhi một ít rượu Bàu Đá cũng đủ để bạn nhớ về hương vị ấy mỗi khi nhắc đến chuyến du lịch này đó. Ngoài ra, nem cũng là lựa chọn thích hợp để làm quà tặng, với hương vị tuyệt vời ấy ai lại lỡ không thích món quà mà bạn tặng.", 20, "nem.png", "Nem chợ huyện", 150000.0, 0 },
                    { 18, 6, 100, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "“Muốn ăn bánh ít lá gai Lấy chồng Bình Định sợ dài đường đi\"Bánh ít lá gai là một trong các đặc sản Bình Định nổi tiếng. Để làm nên những chiếc bánh ít thơm ngon nức tiếng, người làm bánh phải lựa chọn và chuẩn bị những chiếc lá gai rất cầu kỳ vì đây là yếu tố quan trong quyết định đến hương vị của bánh. Kế đến là nếp và nhân cũng được lựa chọn và chế biến từ những nguyên liệu ngon nhất.\n Sau một quá trình xay bột, làm nhân, gói và hấp bánh, những chiếc bánh ít lá gai thơm ngon, dẻo dai với vị ngọt của nhân đậu xanh hoặc nhân dừa đã được ra lò. Với đặc sản này bạn nên thử ít nhất một lần, và đây cũng được xem là một món quà mà chắc chắn người thân của bạn sẽ thích.", 10, "banhit.png", "Bánh ít lá gai", 100000.0, 0 },
                    { 19, 1, 100, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mực rim là món ăn phổ biến khiến du thích yêu thích có mặt ở hầu hết những vùng biển lớn. Tuy nhiên mực rim hay còn gọi là mực ngào Bình Định có một hương vị thơm ngon rất riêng từ vùng biển duyên hải miền Trung. Mực rim Quy Nhơn được người dân làm từ những con mực tươi nhất và hương vị không nơi nào giống được.\n Với hương thơm ngon đặc biệt cùng vị cay cay kích thích vị giác, mực rim trở thành món ăn vặt siêu ngon và được mọi người vô cùng yêu thích. Đồng thời, với những hũ mực rim được làm sẵn giúp bạn có thể dễ dàng lựa chọn đặc sản Bình Định này để làm quà tặng.", 5, "mucrim.png", "Mực rim Quy Nhơn", 150000.0, 0 },
                    { 20, 7, 100, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Với tên gọi độc và lạ của món Chả Tré, món đặc sản Bình Định này đã gợi nên sự tò mò với nhiều du khách muốn tìm hiểu và được thử món ăn độc đáo này. Mặc dù đã có mặt phổ biến khắp các tỉnh thành Trung Trung bộ, nhưng hương vị thơm ngon nhất vẫn là chả Tré Bình Định với cách làm và công thức chỉ vùng đất Bình Định mới làm nên được.\n Thành phần nguyên liệu làm chả Tré cũng tương tự với các loại nem, bì của miền bắc. Nhưng Tré Bình Định được người dân nơi đây khéo léo thay thế bằng nhiều loại nguyên liệu khác như tai heo, lỗ mũi heo, da heo, thịt ba chỉ,...Tré cũng thích hợp để trở thành món quà mang về khi bạn đến thăm Bình Định.", 20, "chatre.png", "Chả Tré rơm", 35000.0, 3 },
                    { 21, 6, 100, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nếu như Hà Nội có bánh cốm, Hải Dương có bánh đậu xanh, Vũng Tàu có bánh bông lan trứng muối,...và những loại bánh làm quà đặc trưng của nhiều tỉnh khác thì Quy Nhơn lại bánh thuẫn nổi tiếng để làm quà tặng cho người thân và bạn bè. Đây cũng là loại bánh phổ biến vào ngày Tết của người dân miền Trung.\n Bánh thuẫn có vị thơm ngon từ nguyên liệu như trứng gà, bột năng, bột bình tinh, đường, đâu ăn, vani và đặc biệt là khuôn đúc bánh. Quá trình đúc bánh bằng than đã góp phần tạo nên được mùi thơm đặc trưng của đặc sản Bình Định này.", 0, "banhthuan.png", "Bánh thuẫn", 15000.0, 0 },
                    { 22, 7, 100, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sở dĩ rượu Bàu đá được biết đến là một trong những đặc sản Bình Định nổi tiếng vì đây là loại rượu không nấu từ gạo thông thường như những loại rượu khác. Rượu Bàu đá Bình Định được nấu từ gạo lứt và chỉ khi sử dụng một nguồn nước trong một làng của tỉnh Bình Định mới đạt được hương vị ngon nhất.\n Từ xưa, rượu Bàu đá đã được tiến cung cho vua nên được xếp vào loại đặc sản thượng hạng của Bình Định. Rượu nổi tiếng dễ say vì có độ cồn rất cao, lên đến 50. Nhưng điều khiến người ta yêu thích hương vị của rượu là vị thanh mát mang lại cảm giác sảng khoái vô cùng. Đây cũng là một món quà thích hợp thể hiện sự kính trọng bạn có thể chọn.", 0, "ruoubauda.png", "Rượu Bầu đá", 40000.0, 0 },
                    { 23, 1, 100, new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Một trong những món ăn phải kể đến đầu tiên trong dah sách những món đặc sản Bình Định đó chính là mực ngào. Mực ngào có một hương vị thơm ngon rất riêng thu hút khách du lịch. Để chế biến được món mực ngào người đầu bếp đã phải rất công phu, tài tình tỉ mỉ chăm chút cho món ăn. Mực sau khi đươc thu mua từ những cảng hải sản tươi ngon được đem về sơ chế và chế biến luôn để giữ được độ tươi ngon nguyên vẹn  của mực.\nMực được  ướp cùng tiêu, tỏi, ớt, mắm và một số loại gia vị khác để tạo độ thơm ngon đặc trưng của mực. Món ăn này có vị cay đặc trưng, thơm thơm của các loại gia vị sẽ làm bạn thích thú và muốn ăn ngay từ cái nhìn đầu tiên. Gía của một cân mực ngào giao động từ  200.000 – 400.000 đồng.", 0, "muc-ngao-ot-dac-san-binh-dinh-lam-qua.jpg", "Mực ngào Bình Định", 250000.0, 0 },
                    { 27, 1, 100, new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cá chỉ vàng là loài cá nước mặn (còn gọi là cá ngân chỉ) thức ăn của chúng là những sinh vật nổi. Thân cá dẹp hình thoi, hai bên có một sọc vàng chạy thẳng từ sau mắt đến gần vây đuôi, phần lưng màu xanh xám, bụng trắng bạc, trên mang cá có chấm đen, vây đuôi vàng, đầu cá hơi nhọn, miệng chếch, hàm dưới nhô ra.\n Cá chỉ vàng thịt trắng có vị ngọt thơm, giàu vitamin B, Omega 3 giúp ngăn ngừa bệnh tim mạch, tốt cho não bộ, cải thiện giấc ngủ...", 0, "cach-lua-ca-chi-vang-kho-ngon.jpg", "Khô cá chỉ vàng", 135000.0, 0 },
                    { 28, 6, 50, new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Khi đến du lịch Bình Định không thể không nhắc tới  món bánh tráng nước dừa. Đây là một món đặc sản nơi xứ dừa. Công đoạn chế biến bánh không quá cầu kì nhưng đòi hỏi sự tỉ mỉ, có kinh nghiệm của người tráng bánh. Nguyên liệu của bánh chủ yếu là Củ Mì ( củ sắn) được sắt nhỏ, xay lấy nước. Cơm dừa được nạo thành sợi nhỏ,  nước dừa và vừng đen. Tất cả đều được đổ chung vào một nồi lớn, trộn đều cho các gia vị hòa quyện cùng với nhau và được đun nóng. Bên cạnh đó có một chảo đang được đun nóng. Khi chảo nóng lên người tráng bánh sẽ dùng một cái gáo làm bằng sọ dừa có cán dài múc từng gáo nước bánh lên chảo và tráng đều. Tráng bánh phải đều tay để cơm dừa và vừng đen được dàn đều mặt bánh. Bánh phải tròn mỏng và không bị chỗ dày, chỗ mỏng thì mới là bánh đạt chuẩn. cứ tráng được mười chiếc  bánh thì đem ra phơi. khi ăn bạn cần nướng lên để bánh có độ phồng và dậy hết mùi thơm của vừng, của nước cốt dừa và cơm dừa. Có thể ăn bánh thay cơm ăn chỉ thấy no mà không thấy chán.", 0, "Banh-trang-nuoc-dua-am-thuc-binh-dinh.jpg", "Bánh tráng nước dừa", 120000.0, 0 },
                    { 29, 1, 50, new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nước mắm nhĩ hay nhỉ còn gọi là nước mắm kéo lù hoặc mắm cốt, là loại nước mắm được hứng từ các giọt nước mắm đầu tiên được “nhỉ” ra. Hay nói cách khác là rò rỉ ra từng giọt, từng giọt từ lỗ van đang đóng kín ở đáy của thùng hay lu vại đang muối cá đã đến thời gian chín có thể lấy nước mắm thành phẩm.", 0, "nuoc-mam-nhi-nguyen-chat-tam-quan-binh-dinh.jpg", "Nước mắm nhĩ Bình Định", 95000.0, 0 },
                    { 30, 1, 50, new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Con ruốc còn gọi là tép biển, tép moi, ở Việt Nam được coi là đặc sản. Chúng là động vật giáp xác 10 chân sống ở vùng nước mặn ven biển hay nước lợ. Ruốc dạng như tôm nhỏ, chỉ lớn khoảng 10–40 mm Do kích thước của con ruốc biển nhỏ, nên thường được dùng để làm nước mắm ruốc (là một loại mắm đặc sản của miền biển) hoặc phơi khô ruốc để chế biến thành các món ăn dân dã đậm đà hương vị biển.", 0, "các-món-từ-ruốc-khô.jpg", "Ruốt khô", 200000.0, 0 },
                    { 31, 1, 50, new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hải sản Quy Nhơn nổi tiếng khắp cả nước với nhiều loại hải sản phong phú đa dạng, trong đó Cá lao là một loại hải sản khô đặc biệt thơm ngon, chúng là một loại cá biển, sau khi được ngư dân đánh bắt được xẻ thịt, phơi khô tạo nên một loại thực phẩm thơm ngon đúng chất tinh túy từ biển.", 0, "cá-lao-khô-quy-nhơn.jpg", "Cá Lao Khô Tẩm Gia Vị", 125000.0, 0 },
                    { 32, 6, 50, new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bánh hồng Tam Quan là một trong những món đặc sản của Bình Định, được xem như biểu trưng cho tin vui, thường xuất hiện trong các dịp cưới hỏi của người dân nơi đây.\n Điều đặc biệt bánh hồng Tam Quan là bánh được làm từ gạo nếp Ngự nổi tiếng dẻo thơm. Do hoàn toàn không có chất bảo quản nên bánh chỉ để được 5 ngày thôi bạn nhé.", 0, "banhhong.jpg", "Bánh hồng Tam Quan", 200000.0, 0 },
                    { 33, 6, 50, new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bánh tráng chả cá là một trong những đặc sản nổi tiếng gần xa của Bình Định. Bánh tráng chả cá được làm từ nguyên liệu chính là cá cùng một ít gia vị và bột năng. Để món ăn đúng vị hơn bạn nên ăn kèm với rau răm nhé.", 0, "banhtrangchaca.jpg", "Bánh tráng chả cá", 400000.0, 0 },
                    { 34, 1, 100, new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nếu đã đến với đất Bình Định thì bạn nhất định phải thử qua món mực ngào vị tỏi nhé. Món ăn là sự hòa quyện giữa vị mực vừa tươi vừa giòn cùng vị cay đặc trưng của ớt và tỏi. Bạn nhớ bảo quản món này ở nhiệt độ thoáng mát nha.", 0, "muc-ngao-ot-dac-san-binh-dinh-lam-qua.jpg", "Mực ngào vị tỏi", 200000.0, 0 },
                    { 35, 1, 45, new DateTime(2022, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chả ram tôm đất là một trong những món ngon đặc sản nổi tiếng của miền đất võ Bình Định, món ăn này phù hợp với mọi lứa tuổi, từ già đến trẻ đều yêu thích và thường xuyên xuất hiện trong các bữa cơm gia đình.\n Miếng chả ram tôm đất Bình Định giòn tan của lớp bánh tráng chiên bên ngoài, bên trong có thịt tôm ngọt tự nhiên, một chút ngầy ngậy của thịt mỡ, tất cả tạo nên hương vị đặc biệt hấp dẫn, gây nghiện cho thực khách khi dùng thử món ăn độc đáo này.", 0, "chả-ram-tôm-đất-quy-nhơn-ngon-loại-1.jpg", "Chả ram tôm đất", 890000.0, 0 },
                    { 36, 1, 44, new DateTime(2022, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ghẹ sữa là ghẹ còn non có kích thước nhỏ, cỡ ngón chân cái người lớn, nhiều nhất vào tháng 5 đến tháng 11, thời điểm ghẹ sinh sản nhiều.\nGhẹ sữa có hàm lượng dinh dưỡng cao, nhiều canxi, đạm, sắt, các vitamin A, B1, B2, C và đặc biệt là magnesium, calcium và axit béo omega 3, có lợi cho sức khỏe và rất tốt cho người có vấn đề tim mạch và hỗ trợ tăng trưởng chiều cao cho trẻ.", 15, "ghe-sua-chien-gion.jpg", "Ghẹ sữa rim tỏi ớt, rang me, chiên giòn", 90000.0, 4 },
                    { 37, 1, 50, new DateTime(2022, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mực một nắng là loại hải sản đặc biệt, để làm mực 1 nắng được ngon, sau khi xẻ phải rửa mực tươi bằng nước biển, rồi phơi dưới trời nắng gắt. Chỉ được phơi qua một nắng để mực vẫn giữ được độ tươi ngon, bên ngoài ráo nước, bên trong dẻo và giòn. \nNhững vùng biển có nước biển càng mặn thì mực 1 nắng sẽ càng ngon, đặc biệt là các khu vực miền Trung. Mực một nắng có nhiều loại, nhưng mực ngon nhất vẫn là làm từ những con mực ống và mực lá.\nĐây là một trong các đặc sản nổi tiếng của Bình Định được du khách tìm mua làm quà.", 20, "muc-mot-nang-gia-bao-nhieu-1kg.jpg", "Mực một nắng", 500000.0, 2 },
                    { 38, 1, 12, new DateTime(2022, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cá đù hay Cá lù đù là một họ cá thuộc bộ Cá vược (Perciformes) có kích thước lớn, chúng sống ở vùng biển nhiệt đới, cận nhiệt đới. Tại vùng biển Việt Nam, có khoảng 270 loài trong 70 chi, đáng kể nhất là cá lù đù bạc chiếm số lượng lớn trong 20 loài như cá lù đù măng đen, cá lù đù lỗ tai đen, cá lù đù kẽm, cá lù đù sóc, cá lù đù đỏ dạ...\nChúng sống thành từng đàn lớn ở gần bờ, thường núp trong những rạn, hốc đá. Thức ăn của chúng là các loại động vật thủy sinh, côn trùng hay cá nhỏ, giáp xác.\n Vì muốn dự trữ được lâu nên sau khi được đánh bắt, ngư dân chọn cá tươi làm sạch, xẻ lóc bỏ xương, bỏ đầu để ráo. Sau đó, đem phơi khô dưới 1 nắng gắt để cá se lại để thịt dẻo dẻo. Hoặc có thể phơi cho thật khô để dự trữ ăn dần.\n Cá đù 1 nắng phần thân sau của cá có nhiều mỡ, rất béo. Loại cá này có vị ngọt dịu deo dẻo và đặc biệt thịt mềm, hậu bùi, có thể chế biến thành nhiều món ngon hấp dẫn. \nHiện nay, đây là đặc sản được rất nhiều người săn lùng, kể cả người nước ngoài cũng rất thích thú với vị ngon ngọt của nó “đặc biệt là giá cả phải chăng”.", 0, "cá-đù-một-nắng.jpg", "Cá đù một nắng", 16000.0, 0 },
                    { 39, 7, 15, new DateTime(2022, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chả Bò (Giò Bò) Bình Định Chính Gốc – Cây 500G", 0, "cha-bo-binh-dinh-nha-lam.jpg", "Chả Bò (Giò Bò) Bình Định Chính Gốc – Cây 500G", 180000.0, 0 },
                    { 40, 6, 20, new DateTime(2022, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đến với Bình Định, du khách sẽ được thưởng thức những món được làm từ các loại bánh tráng. Nào là bánh tráng mè nướng, bánh tráng nước cốt dừa Tam Quan hay bánh tráng bột mì nhứt nướng, bánh tráng gạo nhúng, … loại bánh nào cũng ngon nhứt nách. Hôm nay, Đặc Sản Bình Định Online xin được giới thiệu đến quý khách một loại bánh tráng độc đáo hơn cả đó là bánh tráng nhúng giòn Phù Mỹ. Hãy cùng khám phá bạn nhé. Nếu có cơ hội đến Bình Định hãy thử một lần thưởng thức loại bánh tráng đặc sản Phù Mỹ để tự cảm nhận hương vị thơm ngon đặc trưng của nó nhé.", 0, "banh-trang-nhung-binh-dinh.jpg", "Bánh Tráng Nhúng Giòn Phù Mỹ", 45000.0, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_AdminUserName",
                table: "Admin",
                column: "AdminUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerUserName",
                table: "Customer",
                column: "CustomerUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_CustomerId",
                table: "Favorite",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRating_CustomerId",
                table: "ProductRating",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRating_ProductId",
                table: "ProductRating",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "Favorite");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "ProductRating");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
