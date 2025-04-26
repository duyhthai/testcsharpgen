<?php

namespace Animals;

class Animal {
    public $name;

    public function __construct($name) {
        $this->name = $name . ", created from base";
    }

    public function speak() {
        echo "{$this->name} makes a sound.\n";
    }
}
