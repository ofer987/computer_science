#!/bin/ruby

class Combinations
  def initialize(count)
    @combinations = []
    @count = count
  end

  def append(value)
    unless @combinations.any? { |combination| combination == value }
      @combinations << value
    end
  end
  alias_method :<<, :append
end

# key:
#   integer
#   represents: remaining value
# value:
#   Combination
class Memoization < Hash
  def initialize
    @combinations = Set.new
    @remaining_values = {}
  end

  def append(remaining_value, combination, count)
    combination = Array(combination).sort { |x, y| x <=> y }
    @combinations << combination

    @remaining_values[remaining_value] = count
  end
  alias_method :<<, :append

  def has_key?(key)
    @remaining_values.has_key?(key)
  end
end

# class Memoization < Hash
#   def append(key, count, combination)
#     combination = Array(combination).sort { |x, y| x <=> y }
#     combinations = if has_key?(key)
#                      self[key]
#                    else
#                      Combinations.new(count)
#                    end
#
#     combinations << combination
#   end
#   alias_method :<<, :append
#
#   def combination_used?(key, combination)
#     has_key
#   end
# end

class DeadEnd
  def use_coins
  end

  def count
    0
  end
end

class Leaf
  def initialize
  end

  def use_coins
  end

  def count
    1
  end
end

class Node
  attr_reader :count

  def initialize(coins, value, used_coins, memo)
    # sort in descending order
    @coins = Array(coins).sort { |x, y| x <=> y }
    @value = value
    @used_coins = Array(used_coins)
    @memo = memo

    @count = 0
    @children = []
  end

  def use_coins
    puts "Value: #{@value}"
    if @memo.has_key?(@value, @used_coins)
      puts "Is memoized"
      # @count = @memo[@value]

      return
    end

    expended_all_coins = false
    @children = @coins.map do |coin|
      puts "Using coin: #{coin}"
      new_value = @value - coin
      new_used_coins = @used_coins + Array(coin)
      child = if new_value > 0
                Node.new(@coins, new_value, new_used_coins, @memo)
              elsif new_value == 0
                @memo.append_combination(new_used_coins)
                expended_all_coins = true
                puts "yay"
                Leaf.new
              else
                DeadEnd.new
              end

      child.use_coins

      child
    end

    @count = @children
      .map { |node| node.count }
      .reduce { |val, sum| sum += val }
    if expended_all_coins = 
    puts "Count: #{@count}"

    @memo.append(@value, @count, @used_coins)
  end

  private
end

n, _m = gets.strip.split(' ').map(&:to_i)
coins = gets.strip.split(' ').map(&:to_i)

memo = Memoization.new
counter = Node.new(coins, n, [], memo)
counter.use_coins

puts counter.count
