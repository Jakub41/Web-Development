<?php 

class Validator{

	private $data;

	function setData($name, $value)
	{
		$this->data[$name] = ($value);
	}
	
	function getData($name)
	{
		if (isset($this->data[$name]))
		{
			return $this->data[$name];
		}
		else
		{
			return '';
		}
	}
	
}
?>