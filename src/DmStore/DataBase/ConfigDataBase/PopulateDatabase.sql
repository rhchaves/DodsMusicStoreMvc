/* Consulta das Tabelas Identity AspNet */
 select * from "AspNetUsers";
 select * from "AspNetUserLogins";
 select * from "AspNetUserRoles";
 select * from "AspNetUserTokens";
 select * from "AspNetUserClaims";
 select * from "AspNetRoleClaims";
 select * from "AspNetRoles";

-- Iserir dados para testes

---- FORNECEDORES

INSERT INTO SUPPLIERS(ID,NAME,CNPJ,ADDRESS,ADDRESS_NUMBER,ZIP_CODE,NEIGHBORHOOD,CITY,STATE,STATUS) 
VALUES('36021c0a-88c0-47f7-b09d-99814530e1de','Fender','45634563456354','Avenida Industrial','3643','09645645','Centro','São Bernardo do Campo','SP',1);

INSERT INTO SUPPLIERS(ID,NAME,CNPJ,ADDRESS,ADDRESS_NUMBER,ZIP_CODE,NEIGHBORHOOD,CITY,STATE,STATUS) 
VALUES('ba308e89-bafb-4e94-a300-563c09b45d1b','Gibson','23526457658678','Avenida do Contorno','756','06345345','Campo Belo','Belo Horizonte','MG',1);

INSERT INTO SUPPLIERS(ID,NAME,CNPJ,ADDRESS,ADDRESS_NUMBER,ZIP_CODE,NEIGHBORHOOD,CITY,STATE,STATUS) 
VALUES('3bd402a2-993b-403d-aa67-4af86c9df145','Strinberg','35415844882572','Avenida Paulista','2256','02548642','Centro','São Paulo','SP',1);

INSERT INTO SUPPLIERS(ID,NAME,CNPJ,ADDRESS,ADDRESS_NUMBER,ZIP_CODE,NEIGHBORHOOD,CITY,STATE,STATUS) 
VALUES('01565420-d42b-4040-a525-11e9766e3d25','DW Drums','45846432151686','Avenida Jardins','954','86541578','Centro','Curitiba','PR',1);

INSERT INTO SUPPLIERS(ID,NAME,CNPJ,ADDRESS,ADDRESS_NUMBER,ZIP_CODE,NEIGHBORHOOD,CITY,STATE,STATUS) 
VALUES('482bd964-afe3-430d-a25d-3fecb2b0fb06','Tagima','85564564562131','Avenida João Lotto','5945','89522454','Zona Norte','São Paulo','SP',1);

INSERT INTO SUPPLIERS(ID,NAME,CNPJ,ADDRESS,ADDRESS_NUMBER,ZIP_CODE,NEIGHBORHOOD,CITY,STATE,STATUS) 
VALUES('1d0666f9-ea7b-4cae-92fa-9cd10755aea6','Yamaha','98414552014555','Avenida Tokio','8537','35215457','Industrial','Manaus','AM',1);

COMMIT;

---- PRODUTOS

INSERT INTO PRODUCTS(ID,SUPPLIER_ID,NAME,DESCRIPTION,IMAGE_URI,PRICE,STOCK_QTD,SOLD_QTD,STATUS) 
VALUES('66be4d75-a8a9-4087-b264-be11bae37d9e','36021c0a-88c0-47f7-b09d-99814530e1de','Contra Baixo Fender 013 6860 Deluxe Active Jazz Bass V 341','O contrabaixo Fender Deluxe Active Jazz Bass V tem a sonoridade clássica do Jazz Basss, porém com um mix de características modernas, como o eq. ativo de 3 bandas. Com um par de captadores Noiseless Jazz Bass V e controle de pan, você consegue uma enorme gama de sonoridades. O eq de 3 bandas tem agudo ( + /- 10 db em 8kHz), médio ( + 10dB, -15dB em 500Hz) e grave ( + /- 12db em 40Hz), além disso acompanha um luxuoso Deluxe Gig Bag.','f7a42f39-349e-4cf2-988d-1c1e2b3d886e_Contra-Baixo-Fender-Deluxe.webp',11000,21,0,1);

INSERT INTO PRODUCTS(ID,SUPPLIER_ID,NAME,DESCRIPTION,IMAGE_URI,PRICE,STOCK_QTD,SOLD_QTD,STATUS) 
VALUES('d6a7f6b5-5ab6-4a06-8f8e-85e9d8cd4122','3bd402a2-993b-403d-aa67-4af86c9df145','Guitarra Les Paul Black','Guitarra Les Paul Black','88905c40-d364-45b9-af57-dceb1e715eca_guitarra_les_paul_black.jpg',1500,10,0,1);

INSERT INTO PRODUCTS(ID,SUPPLIER_ID,NAME,DESCRIPTION,IMAGE_URI,PRICE,STOCK_QTD,SOLD_QTD,STATUS) 
VALUES('2d55bfc5-09f6-47e3-8c35-4a0af06fa8b4','36021c0a-88c0-47f7-b09d-99814530e1de','Baixo Square','Baixo Square 5 Cordas Natural Jazz Bass','28901bbb-bd8e-425e-93db-d57b12eba1cb_baixo-square-natural.webp',4000,9,0,1);

COMMIT;

---- USUARIOS

INSERT INTO "AspNetUsers" ("Id","UserName","NormalizedUserName","Email","NormalizedEmail","EmailConfirmed","PasswordHash","SecurityStamp","ConcurrencyStamp","PhoneNumber","PhoneNumberConfirmed","TwoFactorEnabled","LockoutEnd","LockoutEnabled","AccessFailedCount")
VALUES ('8d516fca-cae2-42c5-b120-f1b912dea74a', 'rodolfohenriquechaves@gmail.com', 'RODOLFOHENRIQUECHAVES@GMAIL.COM', 'rodolfohenriquechaves@gmail.com', 'RODOLFOHENRIQUECHAVES@GMAIL.COM', 1, 'AQAAAAIAAYagAAAAED87Uc18bmOTNioJZQ+9Hsqiwu8zZnxhetQpS2oT4AgsDuENvSxcqxN3ogGFnrT4HQ==','JAS7N7SRRJD5ZPXKFPUSXGCLCZWP5TJW', '8db06787-987a-41fb-a38c-335e89d01b36', '11977777777', 1, 0,null, 0, 0);

INSERT INTO CLIENTS (ID,NAME,NORMALIZED_NAME,CPF,PHONE_NUMBER,ADDRESS,ADDRESS_NUMBER,ZIP_CODE,NEIGHBORHOOD,CITY,STATE,STATUS)
VALUES ('8d516fca-cae2-42c5-b120-f1b912dea74a','Rodolfo Chaves','RODOLFO CHAVES','12312312312','11912341234','Rua João Teste',123,09123123,'Centro','São Bernardo do Campo','SP',1);

COMMIT;

