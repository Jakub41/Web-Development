<?php

class UserModel
{
	private $db;
	private $session;
	private $attempt;
	private $clientIP;
	
	function __construct(){
		
		 $this->db = new Database();
		// $this->session = new Session();
	}

	function getUserById($user_Id){

	}
	function confirmIPAddress($loginAttemptsId,$clientIP) {

		$stmt = "SELECT attempts, (CASE when lastlogin is not NULL and DATE_ADD(LastLogin, INTERVAL 1 MINUTE)>NOW() then 1 else 0 end) as Denied
	    FROM loginattempts WHERE loginAttemptsId = ? or ip =?";
	 	$loginAttempt = $this->db->executeStoreProcedure($stmt,array($loginAttemptsId,$clientIP));
	   	$result =$loginAttempt->results();
	 	//print_r($result);
	   //Verify that at least one login attempt is in database

	    if (!$result) {
	    	return 0;
	   	} 
	   if ($result[0]->attempts >= 3){
	      if($result[0]->Denied == 1){
	         return 1;
	      }
	     else
	     {
	     	echo $result[0]->Denied ;
	        $this->clearLoginAttempts($loginAttemptsId,$clientIP);
	        return 0;
	     }
	   }
	   return 0;  
 	 }
   
   function addLoginAttempt($loginAttemptsId,$clientIP) {
	  //echo $this->clientIP;
		  $stmt = "SELECT * FROM loginattempts WHERE loginAttemptsId = ? or ip =?"; 
		  $loginAttempt = $this->db->executeStoreProcedure($stmt,array($loginAttemptsId,$clientIP));
		  $result =$loginAttempt->results();
		  //echo $result[0]->attempts;
		
		  if($result)
	      {
	        $this->attempt = $result[0]->attempts+1;

	        if($this->attempt==3) {
			 $stmt = "UPDATE loginattempts SET attempts=?, lastlogin=NOW() WHERE loginAttemptsId = ? or ip =?";
			  $loginAttempt = $this->db->executeStoreProcedure($stmt,array($this->attempt,$loginAttemptsId,$clientIP));
			 
			}
	        else {
			$stmt = "UPDATE loginattempts SET attempts=?, lastlogin=NOW() WHERE loginAttemptsId = ? or ip =?";
			 $loginAttempt = $this->db->executeStoreProcedure($stmt,array($this->attempt,$loginAttemptsId,$clientIP));
			 
			}
	       }
	      else {
		   $stmt = "INSERT INTO loginattempts (loginAttemptsId,lastLogin,ip,attempts) values (?,NOW(),?,?)";
		   $loginAttempt =$this->db->executeStoreProcedure($stmt,array($loginAttemptsId, $this->clientIP,$this->attempt));
		   
		  }
    }

    function clearLoginAttempts($loginAttemptsId,$clientIP) {
    	$stmt= "UPDATE loginattempts SET attempts = 0 WHERE loginAttemptsId = ? or ip =?"; 
     	$loginAttempt =$this->db->executeStoreProcedure($stmt,array($loginAttemptsId,$clientIP));
	}

	public function validateUser($userName,$password){
		
		$stmt ="CALL SP_Users('','',?,'','','','2015-01-04','','r',@result)";
		$user = $this->db->executeStoreProcedure($stmt,array($userName));
		$this->clientIP =$_SERVER["REMOTE_ADDR"];
		//echo $this->clientIP;
		if (!($user->error())) {
			$results =$user->results();
			$loginAttemptsId = $user->results()[0]->userId;
			

		if($this->confirmIPAddress($loginAttemptsId,$this->clientIP)==0)
		{
			//echo "yes";
			$dbSalt = $results[0]->salt;
			$password = hash('sha512', $password . $dbSalt);	
			if ($password===$results[0]->password) {
			$this->clearLoginAttempts($loginAttemptsId,$clientIP);
		
			Session::setUserName($results[0]->firstName.' '.$results[0]->lastName);
			Session::setUserType($results[0]->userType);
			
			return "valid";
			
		}
		else{
			//echo $this->clientIP;
		
			$attempt=$this->addLoginAttempt($loginAttemptsId,$this->clientIP);
			//echo $attempt;
			return "invalid";
		}
		
		}
		else if ($this->confirmIPAddress($loginAttemptsId,$this->clientIP)==1) {
			//echo 'your account is bloced for 30 minutes';
			return "blocked";
		}
		
		
		
		}
		
	
		else
		return "invalid";
		
		
	}
	public function findAllUsers()
	{
		$stmt ="CALL SP_Users('','',?,'','','','2015-01-04','','rl',@result)";
		$user = $this->db->executeStoreProcedure($stmt,array($userName,$password));
		
	}
	public function addNewUser($userFirstName,$userLastName,$userEmail,
				$password){
		$stmt ="CALL SP_Users('','',?,'','','','2015-01-04','','r',@result)";
		$user = $this->db->executeStoreProcedure($stmt,array($userEmail));
		
		if (!($user->error())) {
			
			return false;
			
			
		}
		else{
				 
				 $salt = hash('sha512', uniqid(mt_rand(1, mt_getrandmax()), true));
				//sha512 hash password with random salt for protection
			$password = hash('sha512', $password . $salt);
				// md5 hash activation key that will be visible in GET response
			$activation = hash('md5', uniqid(mt_rand(1, mt_getrandmax()), true));
				
		$stmt="CALL SP_Users(?,?,?,?,'u',?,DATE_ADD(now(), INTERVAL 2 HOUR),?,'c',@result)";
	
			$user = $this->db->executeStoreProcedure($stmt,array($userFirstName,$userLastName,$userEmail,
				$password,$activation,$salt));
			print_r( $user->results());
			if (!($user->error())) {
				
				return true;
			
			
		}
			

		}
		
		

	}
}

?>