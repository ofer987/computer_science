#!/bin/ruby

require 'set'

# class Combinations
#   def initialize(count)
#     @combinations = []
#     @count = count
#   end
#
#   def append(value)
#     unless @combinations.any? { |combination| combination == value }
#       @combinations << value
#     end
#   end
#   alias_method :<<, :append
# end

# key:
#   integer
#   represents: remaining value
# value:
#   Combination
# class Memoization < Hash
#   def initialize
#     @combinations = Set.new
#     @remaining_values = {}
#   end
#
#   def append(remaining_value, combination, count)
#     combination = Array(combination).sort { |x, y| x <=> y }
#     @combinations << combination
#
#     @remaining_values[remaining_value] = count
#   end
#   alias_method :<<, :append
#
#   def has_key?(key)
#     @remaining_values.has_key?(key)
#   end
# end

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

class Memoization
  attr_accessor :combinations, :counter, :combs

  def initialize
    @combinations = Set.new
    @counter = {}
    @combs = {}
  end

  def has_key?(key)
    @counter.has_key?(key)
  end

  def append(key, value, combinations)
    @counter[key] = value
    @combinations << combinations.sort { |x, y| x <=> y }
  end
end

class DeadEnd
  def use_coins
  end

  def count
    0
  end

  def to_s
    ""
  end

  def used_coin
  end

  def tails
    []
  end

  def combinations
    []
  end
end

class Leaf
  attr_reader :used_coin

  def initialize(used_coins)
    @used_coins = Array(used_coins)
    @used_coin = @used_coins.last
  end

  def use_coins
    # @memo.combs[@used_coin] = [@used_coin]
  end

  def count
    1
  end

  def to_s
    "Leaf"
  end

  def tails
    Array(@used_coin)
  end

  def combinations
    # There is only one combination,
    # because the last node is a leaf
    # sort in ascending order
    @combinatons ||= @used_coins.sort { |x, y| x <=> y }
  end
end

class Node
  attr_reader :count, :used_coin

  def initialize(coins, value, used_coins, memo)
    # sort in descending order
    @coins = Array(coins).sort { |x, y| y <=> x }
    @value = value
    @used_coins = Array(used_coins)
    @new_used_coins = []
    @memo = memo

    @used_coin = @used_coins.last

    @count = 0
    @children = []
  end

  def use_coins
    if @memo.has_key?(@value)
      puts "Has key for value = #{@value}"
      @count = @memo.counter[@value]

      # Add combinations to counter
      new_combs = get_combinations(@used_coins, @memo.combs[@value])
      new_tails = get_combinations([@used_coins.last], @memo.combs[@value])
      puts "The new tails are #{new_tails.to_a}"
      @memo.combinations += new_combs
      @tails = @memo.combs[@value]
      @memo.combs[@value + @used_coin] += new_tails

      return
    end

    @children = @coins.map do |coin|
      puts "Current value = #{@value} using coin #{coin}"
      new_value = @value - coin
      used_coins = @used_coins + Array(coin)
      child = if @used_coins.any? && @used_coins.last < coin
                DeadEnd.new
              elsif new_value > 0
                Node.new(@coins, new_value, used_coins, @memo)
              elsif new_value == 0
                Leaf.new(used_coins)
              else
                DeadEnd.new
              end

      child.use_coins

      child
    end

    @count = @children
      .map { |node| node.count }
      .reduce { |val, sum| sum += val }

    @memo.counter[@value] = @count
    @memo.combinations += combinations
    @memo.combs[@value] = tails

    puts "For #{@value} with coins #{self.combinations} has #{@memo.combinations.count} times"
    @children.select { |node| !node.used_coin.nil? }.each do |node|
      puts "Has child with coin #{node.used_coin}"
    end
    puts "And the value #{@value} has tails of #{@memo.combs[@value]}"
  end

  def to_s
    Array(@used_coin) + @children.flat_map { |node| node.to_s }
  end

  def tails
    # @children.each { |node| puts node.used_coin }
    @tails ||= @children
      .map { |node| node.tails }
      .select { |tails| tails.any? }
      .flat_map { |tails| tails }
  end

  def combinations
    # There are several combinations because its children are subtrees
    # that end in leaves
    @combinations ||= @children.map(&:combinations).select(&:any?)
  end

  private

  def get_combinations(head, tails)
    combinations = tails.map do |tail|
      puts "Head is #{head}"
      puts "tail is #{tail}"
      combination = head + Array(tail)

      combination.sort { |x, y| x <=> y }
    end

    Set.new(combinations)
  end
end

n, _m = gets.strip.split(' ').map(&:to_i)
coins = gets.strip.split(' ').map(&:to_i)

memo = Memoization.new
counter = Node.new(coins, n, [], memo)
counter.use_coins

puts memo.combinations.count
