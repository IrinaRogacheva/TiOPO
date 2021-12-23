Feature: CreateProduct

Scenario: Add product whose category_id is 1
	Given id is 3000
	And category_id is 1
	And title is "new"
	And alias is "alias"
	And content is "content"
	And price is 90
	And old_price is 130
	And status is 0
	And keywords is "keywords"
	And description is "description"
	And hit is 0
	When send create product request
	Then product is created

Scenario: Add product whose category_id is 15
	Given id is 3000
	And category_id is 15
	And title is "тиктак"
	And alias is "alias"
	And content is "content"
	And price is 90
	And old_price is 130
	And status is 0
	And keywords is "keywords"
	And description is "description"
	And hit is 0
	When send create product request
	Then product is created

Scenario: Add product whose category_id is 0
	Given id is 3000
	And category_id is 0
	And title is "тиктак"
	And alias is "alias"
	And content is "content"
	And price is 90
	And old_price is 130
	And status is 0
	And keywords is "keywords"
	And description is "description"
	And hit is 0
	When send create product request
	Then product is not created

Scenario: Add product whose category_id is 16
	Given id is 3000
	And category_id is 16
	And title is "тиктак"
	And alias is "alias"
	And content is "content"
	And price is 90
	And old_price is 130
	And status is 0
	And keywords is "keywords"
	And description is "description"
	And hit is 0
	When send create product request
	Then product is not created

Scenario: Add product whose status is 0
	Given id is 3000
	And category_id is 1
	And title is "тиктак"
	And alias is "alias"
	And content is "content"
	And price is 90
	And old_price is 130
	And status is 0
	And keywords is "keywords"
	And description is "description"
	And hit is 0
	When send create product request
	Then product is created

Scenario: Add product whose status is 1
	Given id is 3000
	And category_id is 1
	And title is "тиктак"
	And alias is "alias"
	And content is "content"
	And price is 90
	And old_price is 130
	And status is 1
	And keywords is "keywords"
	And description is "description"
	And hit is 0
	When send create product request
	Then product is created

Scenario: Add product whose status is -1
	Given id is 3000
	And category_id is 1
	And title is "тиктак"
	And alias is "alias"
	And content is "content"
	And price is 90
	And old_price is 130
	And status is -1
	And keywords is "keywords"
	And description is "description"
	And hit is 0
	When send create product request
	Then product is not created

Scenario: Add product whose status is 2
	Given id is 3000
	And category_id is 1
	And title is "тиктак"
	And alias is "alias"
	And content is "content"
	And price is 90
	And old_price is 130
	And status is 2
	And keywords is "keywords"
	And description is "description"
	And hit is 0
	When send create product request
	Then product is not created

Scenario: Add product whose hit is 0
	Given id is 3000
	And category_id is 1
	And title is "тиктак"
	And alias is "alias"
	And content is "content"
	And price is 90
	And old_price is 130
	And status is 0
	And keywords is "keywords"
	And description is "description"
	And hit is 0
	When send create product request
	Then product is created

Scenario: Add product whose hit is 1
	Given id is 3000
	And category_id is 1
	And title is "тиктак"
	And alias is "alias"
	And content is "content"
	And price is 90
	And old_price is 130
	And status is 0
	And keywords is "keywords"
	And description is "description"
	And hit is 1
	When send create product request
	Then product is created

Scenario: Add product whose hit is -1
	Given id is 3000
	And category_id is 1
	And title is "тиктак"
	And alias is "alias"
	And content is "content"
	And price is 90
	And old_price is 130
	And status is 0
	And keywords is "keywords"
	And description is "description"
	And hit is -1
	When send create product request
	Then product is not created

Scenario: Add product whose hit is 2
	Given id is 3000
	And category_id is 1
	And title is "тиктак"
	And alias is "alias"
	And content is "content"
	And price is 90
	And old_price is 130
	And status is 0
	And keywords is "keywords"
	And description is "description"
	And hit is 2
	When send create product request
	Then product is not created

Scenario: Add product whose title consists of cyrillic
	Given id is 3000
	And category_id is 1
	And title is "классные часы"
	And alias is "klassnye-chasy"
	And content is "content"
	And price is 90
	And old_price is 130
	And status is 0
	And keywords is "keywords"
	And description is "description"
	And hit is 0
	When send create product request
	Then product is created
	When send create product request
	Then check is alias equal to expected

Scenario: Add product whose alias already exists in database
	Given id is 3000
	And category_id is 1
	And title is "test-4000-1"
	And alias is "test-4000-1-0"
	And content is "content"
	And price is 90
	And old_price is 130
	And status is 0
	And keywords is "keywords"
	And description is "description"
	And hit is 0
	When send create product request
	Then product is created
	When send create product request
	Then check is alias equal to expected