Feature: AngularTodoMvc
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: New Item Should Be in List
	Given I have navigated to Angular TodoMvc
	When I have created an Item with text "Some Text"
	Then the item list should contain an item with "Some Text"

Scenario: Should add completed class when item is completed
	Given I have navigated to Angular TodoMvc
	Given I have created an Item with text "Herp Derp"
	When I mark the item as completed
	Then the item should have the "completed" class

Scenario: Should clear completed items after clicking Clear completed
	Given I have navigated to Angular TodoMvc
	Given I have created an Item with text "Item 1"
	Given I have created an Item with text "Item 2"
	Given I have clicked "All" 
	Given I have marked the item "Item 1" completed
	When I click "Clear Completed"
	Then item "Item 1" should be gone
	Then item "Item 2" should exist

Scenario: Should delete existing item
	Given I have navigated to Angular TodoMvc
	Given I have created an Item with text "Herp Derp"
	Given I have clicked "All"
	When I hover over item "Herp Derp"
		And I delete the item with text "Herp Derp"
	Then item "Herp Derp" should be gone

Scenario: should display uncompleted item count
	Given I have navigated to Angular TodoMvc
	Given I have added 5 new Items
	Given I mark an item as completed
	Then the item count should be 4

Scenario: Should edit existing item
	Given I have navigated to Angular TodoMvc
	Given I have created an Item with text "The Real Item"
	When I double click the item
	And insert new text "The Fake Item"
	Then item "The Fake Item" should exist

Scenario: Should hide completed when active displayed
	Given I have navigated to Angular TodoMvc
	Given I have created an Item with text "Blarg"
	Given I have clicked "Active"
	When I have marked the item "Blarg" completed
	Then item "Blarg" should be gone

Scenario: Should show item when completed and completed displayed
	Given I have navigated to Angular TodoMvc
	And I have created an Item with text "Derp1"
	And I have created an Item with text "Derp2"
	When I have marked the item "Derp2" completed
	When I have clicked "Completed"
	Then item "Derp1" should be gone
	And item "Derp2" should exist
