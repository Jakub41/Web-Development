<?php

class Controller
{
    /**
     * @var null Database Connection
     */
    public $db = null;

    
    /**
     * Loads the "model".
     * @return object model
     */
    public function loadModel($model)
    {
        //echo $model;
        require APP . 'model/'.$model.'.php';
        return new $model();
        // create new "model" (and pass the database connection)
        //$this->model = new Model($this->db);
    }
}
