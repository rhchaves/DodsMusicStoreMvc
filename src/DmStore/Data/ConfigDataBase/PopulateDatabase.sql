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

COMMIT;

---- PRODUTOS

INSERT INTO PRODUCTS(ID,SUPPLIER_ID,NAME,DESCRIPTION,IMAGE_URI,PRICE,STOCK_QTD,SOLD_QTD,STATUS) 
VALUES('66be4d75-a8a9-4087-b264-be11bae37d9e','36021c0a-88c0-47f7-b09d-99814530e1de','Contra Baixo Fender 013 6860 Deluxe Active Jazz Bass V 341','O contrabaixo Fender Deluxe Active Jazz Bass V tem a sonoridade clássica do Jazz Basss, porém com um mix de características modernas, como o eq. ativo de 3 bandas. Com um par de captadores Noiseless Jazz Bass V e controle de pan, você consegue uma enorme gama de sonoridades. O eq de 3 bandas tem agudo ( + /- 10 db em 8kHz), médio ( + 10dB, -15dB em 500Hz) e grave ( + /- 12db em 40Hz), além disso acompanha um luxuoso Deluxe Gig Bag.','ImagemTeste',11000,21,0,1);

COMMIT;

---- USUARIOS

INSERT INTO "AspNetUsers" ("Id","UserName","NormalizedUserName","Email","NormalizedEmail","EmailConfirmed","PasswordHash","SecurityStamp","ConcurrencyStamp","PhoneNumber","PhoneNumberConfirmed","TwoFactorEnabled","LockoutEnd","LockoutEnabled","AccessFailedCount")
VALUES ('8d516fca-cae2-42c5-b120-f1b912dea74a', 'rodolfohenriquechaves@gmail.com', 'RODOLFOHENRIQUECHAVES@GMAIL.COM', 'rodolfohenriquechaves@gmail.com', 'RODOLFOHENRIQUECHAVES@GMAIL.COM', 1, 'AQAAAAIAAYagAAAAED87Uc18bmOTNioJZQ+9Hsqiwu8zZnxhetQpS2oT4AgsDuENvSxcqxN3ogGFnrT4HQ==','JAS7N7SRRJD5ZPXKFPUSXGCLCZWP5TJW', '8db06787-987a-41fb-a38c-335e89d01b36', '11977777777', 1, 0,null, 0, 0);

COMMIT;

