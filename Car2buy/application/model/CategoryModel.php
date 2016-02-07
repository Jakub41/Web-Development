<?php

class CategoryModel
{
	private $db;
	
	function __construct(){
		
		 $this->db = new Database();
		
	}

	public function getAllCategories()
	{
		$stmt ="CALL SP_Category('0','','rl',@result)";
		$category = $this->db->executeStoreProcedure($stmt,array());
		
		$categories=$category->results();
		//echo $categories[0]->categoryName;
		return $categories;
	}
	public function addNewCategory()
	{
		
	}
	public function deleteCategory()
	{
		
	}
	public function updateCategory()
	{}
}

?>