<?php

namespace Animals;

require_once 'Animal.php'; // only if you don't use Composer autoload

class Dog extends Animal {
    public function speak() {
        echo "{$this->name} barks.\n";
    }
}
