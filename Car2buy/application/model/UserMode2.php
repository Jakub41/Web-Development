<?php

class UserModel
{
	private $db;
	
	function __construct(){
		
		 $this->db = new Database();
	}

	public function getUserById($user_Id){

	}
	 
	public function validateUser($userName,$password){

		$stmt ="CALL SP_User('','',?,?,'u','','','','r',@result)";
		$user = $this->db->executeStoreProcedure($stmt,array($userName,$password));
		
		$results =$user->results();
		print_r($results);
		if ($results) {
			foreach ($results as $user) {
		
		}
		}
		echo $this->db->error();
		return ($this->db->error())?true:false;
		
	}
	public function findAllUsers()
	{
		$stmt ="CALL SP_User('mahmood','ali',?,?,'','',0,'rl',@result)";
		$user = $this->db->executeStoreProcedure($stmt,array($userName,$password));
		
	}
	public function addNewUser($userFirstName,$userLastName,$userEmail,$password){
		//$stmt ="CALL SP_User('','',?,'','','',0,'r',@result)";
		$stmt="CALL SP_User('','',?,'',?,'','','','v',@result)";
		$user = $this->db->executeStoreProcedure($stmt,array($userEmail,'u'));
		//echo $userFirstName;
		//echo $user->error();
		print_r($user->results());
		//$user = $this->db->executeStoreProcedure($stmt,array($userName));
		if ($user->error()) {
			echo "no error";
			return true;
		
		}
		else print_r($user->results());

	}
	public function deleteUser($userId){
	
	}
	public function updateUser($userId){
	
	}
}

?>