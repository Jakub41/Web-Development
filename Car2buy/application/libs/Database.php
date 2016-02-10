<?php

class Database
{
    private $error=false;
    private $results;
    public $count=0;
    public $pdo;
    
    public function __construct()
    {
           try{$this->pdo = new PDO('mysql:host=localhost;port=3306;dbname=car2buy', 'root', 'SamgYangZend7090');}
           catch(PDOException $e){ die($e->getMessage());}
    }
    public function executeStoreProcedure($stmt,$params=array())
    {
           $this->error=false;
           $sql = $this->pdo->prepare($stmt);
           $pos= 1;
           if(count($params)) {
                foreach ($params as $param) {
                $sql->bindValue($pos, $param);
                $pos++;
                }
            }
            if ($sql->execute()) {
                $this->results= $sql->fetchAll(PDO::FETCH_OBJ);
                $this->error= ($this->count=$sql->rowCount()>0)?$this->error=false: $this->error=true;
             }
            return $this;
    }


       public function error()
       {
               return $this->error;
       }
       public function results()
       {
               return $this->results;
       }
}
?>