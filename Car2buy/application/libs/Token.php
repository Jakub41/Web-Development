<?php
class Token {
	public static function generateToken()
	{
		return Session::setToken('tokenName',md5(uniqid()));
	}

	public static function checkToken($token){
		$tokenName= Session::getToken('tokenName');
		//var_dump($tokenName);
		if ($tokenName===$token) {
			Session::destroySession('tokenName');
			return true;
		}
		return false;
	}
}

?>