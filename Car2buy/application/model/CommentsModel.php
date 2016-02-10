<?php

class CommentsModel
{
	private $db;
	
	function __construct(){
		
		 $this->db = new Database();
		
	}

	public function addNewComment($productId,$comment,$userName,$email){
		$stmt ="CALL SP_Comment(0,?,?,?,?,'c',@result)";
		$comment = $this->db->executeStoreProcedure($stmt,array($productId,$comment,$userName,$email));
		
		//$comments=$commented->results();
		//echo $comments[0]->comment;
		//return $categories;
	}

	public function getAllCommentsByProductId($productId)
	{
		$stmt ="CALL SP_Product(?,'','','','1','rcom',@result)";
		$comment = $this->db->executeStoreProcedure($stmt,array($productId));
		$comments=$comment->results();
		
		return $comments;
	}
}

?>