<?php

namespace Animals;

class Animal {
    public $name;

    public function __construct($name) {
        $this->name = $name;
    }

    public function speak() {
        echo "{$this->name} makes a loud sound.\n";
    }
}
