<?php

class productModel
{
	private $db;
	
	
	function __construct(){
		
		 $this->db = new Database();
		
	}

	public function getAllProducts()
	{
		$stmt ="CALL SP_Product('0','','','',1,'rl',@result)";

		$product = $this->db->executeStoreProcedure($stmt,array());
		$products=$product->results();
		//echo $categories[0]->categoryName;
		return $products;
	}
	public function getProductByCateogryId($categoryId)
	{
		$stmt ="CALL SP_Product('0','','','',?,'rcat',@result)";
		//$stmt = "Select * from product where categoryId=?";
		$product = $this->db->executeStoreProcedure($stmt,array($categoryId));
		$products=$product->results();
		//echo $products[0]->productName;
		return $products;
	}

	public function getProductById($productId)
	{
		//$stmt = "Select * from product where productId=?";
		//$stmt ="CALL SP_Product(?,'','','','1','r',@result)";
		
		$stmt ="CALL SP_Product(?,'','','','1','r',@result)";
		$product = $this->db->executeStoreProcedure($stmt,array($productId));
		$product=$product->results();
		//echo $product->productName;
		//rint_r($product);
		return $product;
	}
	public function addProduct($prodname,$proddescription,$imgpath,$catid)
	{
		$stmt ="CALL SP_Product('0',?,?,?,?,'c',@result)";
		$product = $this->db->executeStoreProcedure($stmt,array($prodname,$proddescription,$imgpath,$catid));
		$product=$product->results();
		if ($product[0]->result=="created") {

			return true;
		
		}
		else return fail;
		
		
	}
}

?>