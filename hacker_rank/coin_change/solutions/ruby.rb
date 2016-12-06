#!/usr/bin/env ruby

require 'pry-byebug'
require 'set'

class Memoization
  attr_accessor :combinations, :counter, :tails

  def initialize
    @combinations = Set.new
    @tails = {}
  end

  def has_key?(key)
    @tails.has_key?(key)
  end
end

class Combination
  def initialize(values = [])
    @values = values
  end

  def +(value)
    new_values = @values + [value]
    new_values.sort! { |x, y| x <=> y }

    self.class.new(new_values)
  end
end

class Combinations
  class << self
    def concat(combinations_array)
      new(
        combinations_array
          .map(&:values)
          .reduce { |sum, values| sum += values }
      )
    end
  end

  attr_reader :values

  def initialize(values = [])
    @values = Set.new(values)
  end

  def add_coin(coin)
    new_values = @values.map do |combination|
      combination + [coin]
    end

    self.class.new(new_values)
  end

  def +(combination)
    @values + [combination]
  end
end

class DeadEnd
  def use_coins
  end

  def used_coin
  end

  def tails
    nil
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
  end

  def tails
    return @tails unless @tails.nil?

    @tails = Combinations.new([ [] ])
  end

  def combinations
    # There is only one combination,
    # because the last node is a leaf
    # sort in ascending order
    @combinatons ||= @used_coins.sort { |x, y| x <=> y }
  end
end

class Node
  attr_reader :used_coin, :tails

  def initialize(coins, value, used_coins, memo)
    # sort in descending order
    @coins = Array(coins).sort { |x, y| y <=> x }
    @value = value
    @used_coins = Array(used_coins)
    @memo = memo

    @used_coin = @used_coins.last

    @children = []

    @used = false
  end

  def use_coins
    if @used
      return
    end
    @used = true

    puts "calculating for #{@used_coin}"

    @children = @coins.map do |coin|
      new_value = @value - coin
      used_coins = @used_coins + Array(coin)
      child = if @used_coins.any? && @used_coins.last < coin
                [DeadEnd.new, nil]
              elsif @memo.tails.has_key?(new_value)
                [@memo.tails[new_value], coin]
              elsif new_value > 0
                [Node.new(@coins, new_value, used_coins, @memo), coin]
              elsif new_value == 0
                [Leaf.new(used_coins), coin]
              else
                [DeadEnd.new, nil]
              end

      child[0].use_coins

      child
    end

    tails

    @memo.tails[@value] = self
    combinations.each do |combination|
      @memo.combinations << combination
    end
  end

  def combinations
    @combinations ||= tails.values.to_a.map { |tail| (tail + @used_coins).sort { |x, y| x <=> y } }
  end

  def tails
    return @tails unless @tails.nil?

    _tails = @children
      .select { |node, coin| !node.tails.nil? }
      .map { |node, coin| node.tails.add_coin(coin) }

    @tails = Combinations.concat(_tails)
  end
end

n, _m = gets.strip.split(' ').map(&:to_i)
coins = gets.strip.split(' ').map(&:to_i)

# n = 4
# coins = [1, 2, 3]
memo = Memoization.new
counter = Node.new(coins, n, [], memo)
counter.use_coins

puts memo.combinations.count
# puts "Combinations: #{memo.combinations.to_a}"
