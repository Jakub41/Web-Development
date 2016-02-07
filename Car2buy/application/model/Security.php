<?php


class Security
{
	
	function full_secx()
	{
		if(isset($_REQUEST)){
			$dis = array('select', 'insert', 'delete', 'update','drop table', 'union', 'null','order by','order+by','from','version','database','tables','query','<','>','<script','/>');
			for ($i = 0; $i < sizeof($_REQUEST); ++$i){
				for ($j = 0; $j < sizeof($dis); ++$j){
					foreach($_REQUEST as $gets){
						if(preg_match (',' . $dis[$j] . ',', strtolower($gets))){
							exit('illegal attack!');
						}
					}
				}
			}
		}
	}

	public function get_secx()
	{
		if(isset($_GET)){
			$dis = array('select', 'insert', 'delete', 'update','drop table', 'union', 'null','order by','order+by','from','version','database','tables','query','<','>','<script','/>');
			for ($i = 0; $i < sizeof($_GET); ++$i){
				for ($j = 0; $j < sizeof($dis); ++$j){
					foreach($_GET as $gets){
						if(preg_match (',' . $dis[$j] . ',', strtolower($gets))){
							exit('illegal attack!');
						}
					}
				}
			}
		}
	}

	public function post_secx()
	{
		if(isset($_POST)){
			$dis = array('select', 'insert', 'delete', 'update','drop table', 'union', 'null','order by','order+by','from','version','database','tables','query','<','>','<script','/>','phpinfo');
			for ($i = 0; $i < sizeof($_POST); ++$i){
				for ($j = 0; $j < sizeof($dis); ++$j){
					foreach($_POST as $gets){
						if(preg_match (',' . $dis[$j] . ',', strtolower($gets))){
							exit('illegal attack!');
						}
					}
				}
			}
		}
	}
}
?>
