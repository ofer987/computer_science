11 January 2018

# Problem

TR did a study.

They found that on average:
Developers do 7 hours of work a day
Managers do 4 hours of work a day
QA do 13 hours of work a day

The CEO has decided to open a new tech center in Thailand.
For some reason, they want to hire a total of 119 people,
and have exactly 1337 hours worked each day.

How many of each type of worker should be hired to best achieve this, according to the study?

# Solution

## Reasoning

x = 119 - y - z
7x + 4y + 13z = 1337
7 * (119 - y - z) + 4y + 13z = 1337
833 - 7y - 7z + 4y + 13z = 1337
(-7 + 4)y + (-7 + 13)z = 1337 - 833
-3y + 6z = 504
6z - 3y = 504

-3y = 504 - 6z
-y = (504 - 6z) / 3
-y = 168 - 2z
y = 2z - 168

Since y has to be a positive integer than we can express z as

y = 2z - 168 > 0
2z > 168
z > 84
84 < z

And because x + y + z = 119 and x and y have to be at least 1 because x > 0 and y > 0 then z <= 117

So, 84 < z <=117

And now we come back to y = 2z - 168, and we can plug different values into z to get y.

We also know that x = 119 - y - z has to be positive, so
119 - y - z > 0
119 > y + z
y < 119 - z
z < 119 - y
And y = 2z - 168
So z < 119 - 2z + 168
3z < 119 + 168
3z < 287
z < 95.6666666
z <= 95

So, 84 < z <= 95

## Answer

Let us write a computer program in Ruby to solve for x and y:

```ruby
#!/usr/bin/env ruby

def get_employees
  (85..95).map do |z|
    y = 2 * z - 168
    x = 119 - y - z

    [x, y, z]
  end
end

puts get_employees
  .map { |employees| "x = #{employees[0]}, y = #{employees[1]}, z = #{employees[2]}" }
  .join("\n")
```
