using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Dal.Migrations
{
    public partial class ModelCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<byte>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PictureUrl = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PublishedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offer_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offer_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offer_CategoryId",
                table: "Offer",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_UserId",
                table: "Offer",
                column: "UserId");

            migrationBuilder.Sql(@"INSERT INTO [User] VALUES 
(1, 'JuanCamiloArcos'),
(2, 'CristianLindo'),
(3, 'NelsonGalindo'),
(4, 'ClaudiaPeña') 
");

            migrationBuilder.Sql(@"INSERT INTO [Category] VALUES 
(1, 'Product'),
(2, 'Service'),
(3, 'I''m looking for')
");

            migrationBuilder.Sql(@"INSERT INTO [Offer] ([Id], [CategoryId], [UserId], [Title], [PictureUrl], [Description], [Location], [PublishedOn]) VALUES 
('4a2c3020-a003-4b44-ad2a-28e03791cd40', 1, 1, 'Linterna LED Recargable Ion-Litio', 'lantern.jpg', 'Linterna de 2000 lumens ligeramente usada. CHIP Cree Xm-l T6 DURACIÓN 100.000 HORAS - ZOOM + 2 BATERIAS RECARGABLES INCLUIDAS + CARGADOR DE PARED DUAL + CUERDA + CONVERTIDOR PILAS AAA - Material Aluminio Anonizado 5 MODOS DE EFECTOS (LUZ ALTA - MEDIA , BAJA, EFECTOS STROBER). Más información y pruebas al 3001234567', 'Bogotá', datetime('2017-02-15T00:00:00.000')),
('53d1872f-e10d-4839-b02e-62f36e1fb1da', 3, 1, 'Puff Pera Lona Impermeable Naranja', 'puff.jpg', 'Tamaño Grande. Fácil Limpieza. Confort. Elegancia. Estilo Novedoso.', 'Bogotá', datetime('2017-10-03T00:00:00.000')),
('eac403ab-e162-4e7d-abf5-6a8ca2f00482', 2, 2, 'Carpooling', 'carpooling.jpg', 'A quien le sirva los dias pares tomo la ruta adjunta, usualmente salgo alrededor de las 7 a.m. en la manana y a las 5 p.m. en la tarde en el sentido opuesto, si estan por la ruta o sus alrededores podemos coordinar.', 'Bogotá', datetime('2017-05-17T00:00:00.000')),
('6711ed93-c0fa-48f8-a764-b4559671e4b7', 1, 1, 'Sofacama Amoblando George Con Brazos Café', 'sofa.jpg', 'Medida sentado (Ancho: 180cm) (Largo: 100cm) (Alto: 80cm) (aprox). Medida acostado (Ancho: 180cm) (Largo: 110cm) (Alto: 40cm) (aprox). Sofacama de 3 posiciones en su espaldar: sentado, ver tv y acostado. Entrega a convenir.', 'Bogotá', datetime('2017-08-15T00:00:00.000')),
('d4d2424a-54b7-4937-b99c-de1e92c24c89', 2, 2, 'Casa Campestre Conjunto Cerrado', 'summer-house.jpg', 'Hermosa casa campestre en lote de 2.300 m2 con todo PRIVADO Y EXCLUSIVO Piscina, Jacuzzi, Cancha múltiple, seguro y tranquilo a 10 Minutos del Parque, a 2 Minutos del centro y a 20 Minutos del aeropuerto de Armenia El Eden. Informes', 'Armenia', datetime('2017-03-22T00:00:00.000')),
('725fb2a7-7e17-47a1-9fb2-f7e7fba8fc4c', 3, 3, 'Computador Apple II vintage', 'apple2.jpg', 'Busco un Apple II o Apple IIc que funcione. De preferencia que incluya monitor y disqueteras.', 'Bogotá', datetime('2017-08-09T00:00:00.000')),
('725fb2a7-7e17-47a1-9fb2-f7e7fba8fc41', 3, 3, 'Test item name 1', 'iphone.jpg', 'Test item description 1.', 'Bogotá', datetime('2017-08-09T00:00:00.000')),
('725fb2a7-7e17-47a1-9fb2-f7e7fba8fc42', 3, 3, 'Test item name 2', 'iphone.jpg', 'Test item description 2.', 'Bogotá', datetime('2017-08-09T00:00:00.000')),
('725fb2a7-7e17-47a1-9fb2-f7e7fba8fc43', 3, 3, 'Test item name 3', 'iphone.jpg', 'Test item description 3.', 'Bogotá', datetime('2017-08-09T00:00:00.000')),
('725fb2a7-7e17-47a1-9fb2-f7e7fba8fc44', 3, 3, 'Test item name 4', 'iphone.jpg', 'Test item description 4.', 'Bogotá', datetime('2017-08-09T00:00:00.000')),
('725fb2a7-7e17-47a1-9fb2-f7e7fba8fc45', 3, 3, 'Test item name 5', 'iphone.jpg', 'Test item description 5.', 'Bogotá', datetime('2017-08-09T00:00:00.000'))
");

        }            
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
