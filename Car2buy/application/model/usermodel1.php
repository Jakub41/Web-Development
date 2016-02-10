<?php

class UserModel
{
	public $db;
	function __construct(){
		//$this->dbcon=new Database();
		 $this->db = new Database();
	}

	public function getUserById($user_Id){

	}
	 public function querys($stmt,$params=array())
	 {
	 	$sql = $this->db->pdo->prepare($stmt);
		$pos= 1;
		if(count($params)) {
			foreach ($params as $param) {
		$sql->bindParam($pos, $param,PDO::PARAM_INPUT_OUTPUT);
    	 $pos++;
			}
		
		//	
    
   // 
}
print_r( $sql);

$result=$sql->execute();
   	 	$row = $sql->fetch(PDO::FETCH_ASSOC);
echo $row['result'];
	 	$arr = str_split($stmt);
		//print_r( $arr);
		//echo count($arr);
		for ($i=0; $i <count($arr) ; $i++) { 
			if (in_array("?", $arr)) {
			//echo "yes";
			//$key = array_search("?", $arr);
			//echo $key;
			//$bind =str_replace("?", $params[$i], $arr[$i]);
			
			
			//echo $bind;
		
		}
		
		//echo $bind;
		}
		//return $bind;
	 }
	public function validateUser($userName,$password){



		//$result =$this->db->query( "CALL SP_User", array('','',$userName,$password,'','',0,'r','@result'));
		//$result =$this->db->query( "select * from users where email =? and password=?", array($userName,$password));
		//$sql ="CALL SP_User('','','?','?','','',0,'r',@result)";

	//	$sql ="select * from users where email = 'ali'";
		$stmt ="CALL SP_User('mahmood','ali',?,?,'','',0,'u',@result)";
		$q=$this->querys($stmt,array($userName,$password));
		echo $q;
		$arr = str_split($stmt);
		//print_r( $arr);
		//echo count($arr);
		for ($i=0; $i <count($arr) ; $i++) { 
			if (in_array("?", $arr)) {
			//echo "yes";
			//$key = array_search("?", $arr);
			//echo $key;
			$bind =str_replace("?", $userName, $arr[$i]);
			
			
			# code...
		}
	//	echo $bind;
		}
		//print_r($bind);
		
		//$stmt ="CALL SP_User(?,?)";
		//"CALL SP_User('','','$sEmail','$sPassword','','',0,'r',@result)";
		$sql = $this->db->pdo->prepare($stmt);
		 //$sql->bindParam(1, 'a',PDO::PARAM_STR);
    // $stmt->bindParam(2, 'a',PDO::PARAM_STR);
		if($stmt) {
			$user = $this->db->pdo->quote($userName);
			
    $sql->bindParam(1, $userName,PDO::PARAM_INPUT_OUTPUT);
    $sql->bindParam(2, $password, PDO::PARAM_INPUT_OUTPUT);
    //$sql->bindParam(5, '');
    // $sql->bindParam(6, '');
    //$sql->bindParam(7, 1,PDO::PARAM_STR);
    //$sql->bindParam(8, 'r',PDO::PARAM_STR);
  //  $sql->bindParam(9, '@result',PDO::PARAM_STR);
   
    $result=$sql->execute();
   	 	$row = $sql->fetch(PDO::FETCH_ASSOC);
    	
    	//echo $row['id'] .$row['first_name'].$row['result'];
    	//echo $row['user_type'];
    
    	//echo $row['result'];
    
}
 {
       // echo "Error: " ;
    }

    //echo $result;
		//$result = $this->db->pdo->prepare($sql);
		//if ($result->execute()) {
		//	if ($result->rowCount()>0) {
	//	echo $result->rowCount();
	//	return true;
			//}
		
		//}
		
		//echo $result->count;
		//echo $result->error();
		//return ($result->error())?false:true;


		
	}
	public function findAllUsers()
	{
		//global $database;
		
		
		
		$result =$this->db->query("Select * from user");
		 $row = $result->fetch_assoc();
		return $result;
	}
	public function addUser($User){

	}
}

?>