﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace NetOpnApiBuilder.Migrations
{
    public partial class InitializeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiModules",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApiName = table.Column<string>(maxLength: 100, nullable: false),
                    ClrName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiModules", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ApiObjectTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    ApiVersion = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiObjectTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TestDevice",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Host = table.Column<string>(maxLength: 200, nullable: false),
                    Key = table.Column<string>(maxLength: 200, nullable: false),
                    Secret = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestDevice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ApiControllers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApiName = table.Column<string>(maxLength: 100, nullable: false),
                    ClrName = table.Column<string>(maxLength: 100, nullable: true),
                    ModuleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiControllers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ApiControllers_ApiModules_ModuleID",
                        column: x => x.ModuleID,
                        principalTable: "ApiModules",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiObjectProperties",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ObjectTypeID = table.Column<int>(nullable: false),
                    ApiName = table.Column<string>(maxLength: 100, nullable: false),
                    ClrName = table.Column<string>(maxLength: 100, nullable: true),
                    DataType = table.Column<int>(nullable: false),
                    DataTypeObjectTypeID = table.Column<int>(nullable: true),
                    CanBeNull = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiObjectProperties", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ApiObjectProperties_ApiObjectTypes_DataTypeObjectTypeID",
                        column: x => x.DataTypeObjectTypeID,
                        principalTable: "ApiObjectTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ApiObjectProperties_ApiObjectTypes_ObjectTypeID",
                        column: x => x.ObjectTypeID,
                        principalTable: "ApiObjectTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiCommands",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApiName = table.Column<string>(maxLength: 100, nullable: false),
                    ClrName = table.Column<string>(maxLength: 100, nullable: true),
                    UsePost = table.Column<bool>(nullable: false),
                    ControllerID = table.Column<int>(nullable: false),
                    PostBodyObjectTypeID = table.Column<int>(nullable: true),
                    ApiVersion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiCommands", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ApiCommands_ApiControllers_ControllerID",
                        column: x => x.ControllerID,
                        principalTable: "ApiControllers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApiCommands_ApiObjectTypes_PostBodyObjectTypeID",
                        column: x => x.PostBodyObjectTypeID,
                        principalTable: "ApiObjectTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ApiQueryParams",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommandID = table.Column<int>(nullable: false),
                    ApiName = table.Column<string>(maxLength: 100, nullable: false),
                    ClrName = table.Column<string>(maxLength: 100, nullable: true),
                    DataType = table.Column<int>(nullable: false),
                    AllowNull = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiQueryParams", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ApiQueryParams_ApiCommands_CommandID",
                        column: x => x.CommandID,
                        principalTable: "ApiCommands",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiUrlCommands",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommandID = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    DataType = table.Column<int>(nullable: false),
                    AllowNull = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiUrlCommands", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ApiUrlCommands_ApiCommands_CommandID",
                        column: x => x.CommandID,
                        principalTable: "ApiCommands",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiCommands_PostBodyObjectTypeID",
                table: "ApiCommands",
                column: "PostBodyObjectTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ApiCommands_ControllerID_ApiName",
                table: "ApiCommands",
                columns: new[] { "ControllerID", "ApiName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiControllers_ModuleID_ApiName",
                table: "ApiControllers",
                columns: new[] { "ModuleID", "ApiName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiModules_ApiName",
                table: "ApiModules",
                column: "ApiName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiObjectProperties_DataTypeObjectTypeID",
                table: "ApiObjectProperties",
                column: "DataTypeObjectTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ApiObjectProperties_ObjectTypeID_ApiName",
                table: "ApiObjectProperties",
                columns: new[] { "ObjectTypeID", "ApiName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiObjectTypes_Name",
                table: "ApiObjectTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiQueryParams_CommandID_ApiName",
                table: "ApiQueryParams",
                columns: new[] { "CommandID", "ApiName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiUrlCommands_CommandID_Order",
                table: "ApiUrlCommands",
                columns: new[] { "CommandID", "Order" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiObjectProperties");

            migrationBuilder.DropTable(
                name: "ApiQueryParams");

            migrationBuilder.DropTable(
                name: "ApiUrlCommands");

            migrationBuilder.DropTable(
                name: "TestDevice");

            migrationBuilder.DropTable(
                name: "ApiCommands");

            migrationBuilder.DropTable(
                name: "ApiControllers");

            migrationBuilder.DropTable(
                name: "ApiObjectTypes");

            migrationBuilder.DropTable(
                name: "ApiModules");
        }
    }
}
