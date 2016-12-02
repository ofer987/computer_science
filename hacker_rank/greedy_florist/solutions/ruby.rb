class Flower
  attr_reader :cost

  def initialize(cost)
    @cost = cost.to_i
  end
end

class Person
  def initialize
    @purchased_count = 0
  end

  def purchase(flower)
    @purchased_count += 1

    # puts "Purchasing #{flower.cost} with a history of #{@purchased_count} "
    @purchased_count * flower.cost
  end
end

class Manager
  def initialize(flower_costs, person_count)
    @flowers = Array(flower_costs)
      .map { |cost| Flower.new(cost) }
      .sort { |x, y| x.cost <=> y.cost }
      .reverse

    # @flowers.each do |flower|
    #   puts flower.cost
    # end

    @persons = person_count.to_i.times.map do
      Person.new
    end
  end

  def purchase_flowers
    purchased = 0
    paid = 0

    # initial
    flower = @flowers[0]
    person = @persons[0]

    while !flower.nil?
      # computation
      paid += person.purchase(flower)
      purchased += 1

      # next
      flower = @flowers[purchased]
      person = @persons[purchased % @persons.count]
    end

    paid
  end
end

_flowers_count, person_count = gets.split(' ')
flower_costs = gets.split(' ')

manager = Manager.new(flower_costs, person_count)

puts manager.purchase_flowers
