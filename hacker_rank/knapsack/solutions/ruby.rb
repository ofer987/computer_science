#!/usr/bin/env ruby

class Sack
  def initialize(coins, sum)
    @coins = coins
    @sum = sum
  end
end

cases = gets.to_i

cases.times.each do
  _coin_count, sum = gets.split(' ').map(&:to_i)
  coins = get.split(' ')

  Sack.new(coins, sum)
end
