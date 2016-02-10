CREATE DATABASE  IF NOT EXISTS `car2buy` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `car2buy`;
-- MySQL dump 10.13  Distrib 5.5.16, for Win32 (x86)
--
-- Host: localhost    Database: car2buy
-- ------------------------------------------------------
-- Server version	5.5.24

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `category` (
  `categoryId` int(11) NOT NULL AUTO_INCREMENT,
  `categoryName` varchar(45) NOT NULL,
  `categoryDescription` varchar(245) DEFAULT NULL,
  PRIMARY KEY (`categoryId`),
  UNIQUE KEY `categoryId_UNIQUE` (`categoryId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` VALUES (1,'Honda',NULL),(2,'Suzuki',NULL),(3,'Toyota',NULL),(4,'Chevrolet',NULL);
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `comment`
--

DROP TABLE IF EXISTS `comment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `comment` (
  `commentId` int(11) NOT NULL AUTO_INCREMENT,
  `productId` int(11) NOT NULL,
  `comment` text NOT NULL,
  `name` varchar(45) NOT NULL,
  `email` varchar(45) NOT NULL,
  PRIMARY KEY (`commentId`)
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comment`
--

LOCK TABLES `comment` WRITE;
/*!40000 ALTER TABLE `comment` DISABLE KEYS */;
INSERT INTO `comment` VALUES (1,1,'good one','mahmood','mahmood@gmail.com'),(2,10,'nbv','mahmood','mahmood_std@hotmail.com'),(3,10,'nbv','mahmood','mahmood_std@hotmail.com'),(4,10,'nbv','mahmood','mahmood_std@hotmail.com'),(17,1,'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure.','mahmood','mahmood_std@hotmail.com'),(18,1,'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure.','mahmood','mahmood_std@hotmail.com'),(28,4,'aaa','mahmood','mahmood_std@hotmail.com'),(29,4,'s','mahmood','mahmood_std@hotmail.com'),(30,1,'hello','mahmood','mahmood_std@hotmail.com'),(31,2,'ghdfsadfgs','mahmood','mahmood_std@hotmail.com'),(32,10,'aaa','hali121-001 ','mahmood_std@hotmail.com'),(33,3,'safads','mahmood','jeans025@gmail.com'),(34,3,'asfd','mahmood','mahmood_std@hotmail.com'),(35,6,'aa','mahmood','mahmood_std@hotmail.com'),(36,3,'&lt;script&gt;alert(&#039;hacked&#039;)&lt;/script&gt;','mahmood','mahmood_std@hotmail.com'),(37,3,'hello','mahmood','1a@hotmail.com'),(38,3,'hej','mahmood','ali@hotmail.com');
/*!40000 ALTER TABLE `comment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product` (
  `productId` int(11) NOT NULL AUTO_INCREMENT,
  `productName` varchar(45) NOT NULL,
  `productDescription` text,
  `categoryId` int(11) NOT NULL,
  `imagePath` varchar(145) DEFAULT NULL,
  PRIMARY KEY (`productId`)
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES (1,'Civic',NULL,1,'pic.jpg'),(2,'Civic',NULL,1,'pic1.jpg'),(3,'Civic',NULL,1,'pic2.jpg'),(4,'Civic',NULL,2,'pic3.jpg'),(5,'Civic',NULL,2,'pic4.jpg'),(6,'Civic',NULL,2,'pic5.jpg'),(7,'Civic',NULL,2,'pic6.jpg'),(8,'Civic',NULL,3,'pic2.jpg'),(9,'Civic',NULL,3,'pic.jpg'),(10,'Civic',NULL,3,'pic1.jpg'),(50,'CVIC','new model',4,'a.jpg'),(51,'CVIC','new model',4,'b.jpg');
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `userId` int(11) NOT NULL AUTO_INCREMENT,
  `firstName` varchar(45) DEFAULT NULL,
  `lastName` varchar(45) DEFAULT NULL,
  `email` varchar(45) NOT NULL,
  `password` varchar(200) NOT NULL,
  `expirationTime` timestamp NULL DEFAULT NULL,
  `salt` varchar(150) NOT NULL,
  `userType` varchar(45) NOT NULL,
  `activationKey` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`userId`),
  UNIQUE KEY `email_UNIQUE` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'mahmood','ali','m@gmail.com','ali','2015-01-03 23:00:00','34rf89yuhj','a',NULL),(19,'mahmood','ali','ali@gmail.com','c49fbb1ff78407d8f2f73a5c3345b87e9c9c41a9446a7164bd56af92b3ad21593759c2f2af000688f5bb58138283ecc476f1996a8b428dfcf4b8e2397fb783fc','2015-12-18 20:39:39','8ced7eada12982bde8902b39d6628fed28a96a72ec7f6c93f30156bcf17249fcb8e80594ba05074267dec2bbe0dc041128f1e49b659748ecddcad6c7ebae1c79','a','27209379f5c26fabd22579e8f961c492'),(36,'mahmood','ali','m12@gmail.com','e850524b1585a7768f3f8523a569014d4e6a492f5eef99b0e433c8e73052c6f8d080ab9708e76461e9eb0b032925c6b35907348d1fdb4c3ec6c783cd3bc0d50b','2015-12-21 12:14:18','238573ba59747b562e4548b50f21bfe6954fcadaf8a5a4067731aa4cece798a766f18d5ff67a06dc91aa66bc9dfc0b6d941cf77a19151ab44ca6debf1cd574e2','u','a67523d4f177f70a05cd38155090e987'),(37,'mahmood','aa','m1@gmail.com','0898e55f05ad596d6a78c749801cd624fe2f50a2cc63b89ca054b4c38a5f7d760846c1f06c7889f06d1a4ac97ae316f1c728c24f03ff714a928e15f02d7f201e','2015-12-21 12:51:54','eeea31b8883a2d8c7df70626be307145937ad159d10bf98d28e11ec2476a6becc8e6c48976799235a85e79b19f2e86833c0f9c23fc0b77fc591116f4fc231b46','u','8b8b87570b5e1e70268c4875aea91a30');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `loginattempts`
--

DROP TABLE IF EXISTS `loginattempts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `loginattempts` (
  `loginAttemptsId` int(11) NOT NULL,
  `lastLogin` datetime DEFAULT NULL,
  `ip` varchar(145) DEFAULT NULL,
  `attempts` int(11) DEFAULT NULL,
  PRIMARY KEY (`loginAttemptsId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `loginattempts`
--

LOCK TABLES `loginattempts` WRITE;
/*!40000 ALTER TABLE `loginattempts` DISABLE KEYS */;
INSERT INTO `loginattempts` VALUES (19,'2016-01-06 19:17:04','::1',1);
/*!40000 ALTER TABLE `loginattempts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orderlist`
--

DROP TABLE IF EXISTS `orderlist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `orderlist` (
  `orderListId` int(11) NOT NULL AUTO_INCREMENT,
  `productId` int(11) NOT NULL,
  `orderId` int(11) NOT NULL,
  `status` varchar(45) DEFAULT NULL,
  `quantity` int(11) DEFAULT NULL,
  `cost` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`orderListId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderlist`
--

LOCK TABLES `orderlist` WRITE;
/*!40000 ALTER TABLE `orderlist` DISABLE KEYS */;
/*!40000 ALTER TABLE `orderlist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `order`
--

DROP TABLE IF EXISTS `order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `order` (
  `orderId` int(11) NOT NULL AUTO_INCREMENT,
  `userId` int(11) NOT NULL,
  PRIMARY KEY (`orderId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order`
--

LOCK TABLES `order` WRITE;
/*!40000 ALTER TABLE `order` DISABLE KEYS */;
/*!40000 ALTER TABLE `order` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'car2buy'
--
/*!50003 DROP PROCEDURE IF EXISTS `SP_Cat` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `SP_Cat`(in catid int)
BEGIN
select categoryName from category where categoryId=catid ;

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SP_Category` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `SP_Category`(in catid Int,in catname varchar(45),in Action varchar(5),out result varchar(50))
BEGIN
    case Action
       
        when 'rl' then
        select * from category;
        when 'r' then
    
        select * from category where categoryId=catid;
       when 'c' THEN 
		
			insert into category (categoryName) values(catname);
						
			set result ='created';
			select result;
		when 'u' then
		
			update category set categoryName=categoryname where categoryId=catid;
			SET result ='updated';
			SELECT result;
		when 'd'then
		
			delete  from category where categoryId=catid ;
	END case;
        
     
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SP_Comment` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `SP_Comment`(in comId int, prodId int, in com text,in username varchar(45), in mail varchar(45)
,in Action varchar(5), out result varchar(50))
BEGIN

case Action
      when 'c' then
      insert into comment(productId,comment,name,email) values(prodId,com,username,mail);
	END case;
        
     
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SP_Product` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `SP_Product`(in prodid Int,in prodname varchar(45),in proddescription text,in imgpath varchar(145),
in catid Int, in sAction varchar(5),out result varchar(50))
BEGIN
declare latest_id int default 0;
case sAction
        when 'rl' then
        
        select * from product;
        when 'r' then
        select * from product where productid = prodid;
        when 'rcat' then
        select * from product where categoryId = catid; 
        when 'rcom' then
        select *from comment inner join product on comment.productId = product.productId where product.productId=prodid;
       when 'c' THEN 
		
			insert into product (productName,productDescription,imagePath,categoryId) values(prodname,proddescription,imgpath,catid);
						
			set result ='created';
			select result;
		when 'u' then
		
			update user set productName=prodname, productDescription=proddescription,imagePath =imgpath where productId =prodid;
			SET result ='updated';
			SELECT result;
		when 'd'then
		
			delete  from product where productId =prodid;
	END case;
        
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SP_Users` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `SP_Users`(in firstname varchar(64),in lastname varchar(64), 
            in mail varchar(45), in pass varchar(200),in userType varchar(5),in activation varchar(100),
            in expirationTime timestamp,in salt varchar(150),in sAction varchar(5),out result varchar(50))
BEGIN
declare latest_id int default 0;
    
	case sAction
	
		when 'r'Then 
       
		SELECT * FROM user WHERE email=mail;
		when 'v'Then 
    SELECT * FROM user WHERE email=mail ;
    SET result ='exists';
		SELECT result;
		WHEN 'rl'THEN 
		SELECT * FROM user;
		SET result ='created';
		SELECT result;
		when 'c' THEN 
		
			insert into user (firstName,lastName,email, password,userType,activationKey,expirationTime,salt)
			values(firstname,lastname,mail, pass,usertype,activation,expirationTime,salt);
			SELECT last_insert_id() INTO latest_id;
			
			set result ='created';
			select result;
		when 'u' then
		
			update user set first_name=firstname, last_name=lastname, password= pass where email=mail;
			SET result ='updated';
			SELECT result;
		when 'd'then
		
			delete  from user where email=mail;
	END case;
    END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-01-06 22:28:09
