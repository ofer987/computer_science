#!/bin/ruby

class IceCream
  attr_reader :flavour, :cost

  def initialize(flavour, cost)
    @flavour = flavour
    @cost = cost
  end

  def ==(other)
    flavour != other.flavour && cost == other.cost
  end
end

class NilIceCream
  def nil?
    true
  end
end

class BinarySearch
  attr_reader :icecreams

  def initialize(values)
    @icecreams = to_icecreams(values).sort_by { |icecream| icecream.cost }
  end

  def recursive_search(icecream, start_index, end_index)
    return NilIceCream.new if start_index >= end_index

    mid_index = start_index + ((end_index - start_index) / 2)
    mid_element = icecreams[mid_index]
    return mid_element if mid_element == icecream

    mid_element.cost > icecream.cost ?
      recursive_search(icecream, start_index, mid_index) :
      recursive_search(icecream, mid_index+1, end_index)
  end

  private

  def to_icecreams(values)
    icecreams = []
    values.each_with_index do |cost, flavour|
      icecreams << IceCream.new(flavour, cost)
    end

    icecreams
  end
end

trip_count = gets.strip.to_i
for trip in (0...trip_count)
  m = gets.strip.to_i
  n = gets.strip.to_i
  a = gets.strip.split(' ').map(&:to_i)

  binary_searcher = BinarySearch.new(a)

  for first_flavour in 0...a.length
    first_cost = a[first_flavour]
    first_icecream = IceCream.new(first_flavour, a[first_flavour])
    second_cost = m - first_icecream.cost
    search_icecream = IceCream.new(first_flavour, second_cost)

    second_icecream = binary_searcher.recursive_search(search_icecream, 0, n-1)

    unless second_icecream.nil?
      puts "#{first_icecream.flavour+1} #{second_icecream.flavour+1}"
      break
    end
  end
end
