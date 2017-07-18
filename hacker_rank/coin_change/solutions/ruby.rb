#!/usr/bin/env ruby

class Node
  def initialize(used_coins, value, coins, memo)
    self.used_coins = used_coins.sort { |first, second| first <=> second }
    self.value = value

    self.coins = coins
    self.memo = memo
  end

  def count
    if value == 0
      1
    elsif memo.has_key?(used_coins)
      memo.fetch(used_coins)
    else
      memo[used_coins] = coins
        .reject { |c| value - c < 0 }
        .map { |c| Node.new(used_coins + [c], value - c, coins, memo).count }
        .reduce(:+) || 0
    end
  end

  private

  attr_accessor :used_coins, :value, :coins, :memo
end

value = gets.strip.split(' ').first.to_i
coins = gets
  .strip
  .split(' ')
  .map(&:to_i)
  .sort { |first, second| first <=> second }

memo = {}
counter = Node.new([], value, coins, memo)
puts counter.count

