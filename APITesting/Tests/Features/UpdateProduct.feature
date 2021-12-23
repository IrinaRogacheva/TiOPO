Feature: UpdateProduct

Scenario: Update product with category_id to update is 1
	Given id to update is 5844
	And category_id to update is 1
	When send update product request
	Then category_id is updated

Scenario: Update product with category_id to update is 15
	Given id to update is 6141
	And category_id to update is 15
	When send update product request
	Then category_id is updated

Scenario: Update product with category_id to update is 0
	Given id to update is 5889
	And category_id to update is 0
	When send update product request
	Then category_id is not updated

Scenario: Update product with category_id to update is 16
	Given id to update is 7879
	And category_id to update is 16
	When send update product request
	Then category_id is not updated

Scenario: Update product with status to update is 0
	Given id to update is 5891
	And status to update is 0
	When send update product request
	Then status is updated

Scenario: Update product with status to update is 1
	Given id to update is 6066
	And status to update is 1
	When send update product request
	Then status is updated

Scenario: Update product with status to update is -1
	Given id to update is 7917
	And status to update is -1
	When send update product request
	Then status is not updated

Scenario: Update product with status to update is 2
	Given id to update is 6072
	And status to update is 2
	When send update product request
	Then status is not updated

Scenario: Update product with hit to update is 0
    Given id to update is 6089
	And hit to update is 0
	When send update product request
	Then hit is updated

Scenario: Update product with hit to update is 1
	Given id to update is 6093
	And hit to update is 1
	When send update product request
	Then hit is updated

Scenario: Update product with hit to update is -1
	Given id to update is 6094
	And hit to update is -1
	When send update product request
	Then hit is not updated

Scenario: Update product with hit to update is 2
	Given id to update is 6119
	And hit to update is 2
	When send update product request
	Then hit is not updated