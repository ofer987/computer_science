# class Number
#   attr_reader :value
#
#   def initialize(value)
#     @value = value
#   end
#
#   def prime?
#     return false if value < 2
#     return true if value == 2
#
#     return !(2..value-1).any? { |den| value % den == 0 }
#   end
# end

class Primes
  attr_reader :semiprimes

  def initialize
    @storage = []
    @semiprimes = Semprimes.new
    # @count = count
  end

  # assumes that the number is a prime
  def <<(prime)
    last = storage.last || 0
    return if last >= prime

    new_primes = PrimeFinder.new.find(last+1, prime)
    new_semiprimes = SemiprimeFinder.new.add(new_primes, storage)

    storage += new_primes
    semiprimes += new_semiprimes
  end
  alias_method :append, :<<

  private

  attr_reader :storage
end

class PrimeFinder
  def find(start, last)
    primes = []
    (start..last).each do |value|
      primes << value if prime?(value)
    end

    primes
  end

  def prime?(value)
    return false if value < 2
    return true if value == 2

    return !(2..value-1).any? { |den| value % den == 0 }
  end
end

class SemiprimeFinder
  # def find(first, *primes)
  #   primes = Array(primes)
  #   primes.each do |i|
  #     primes.each do |j|
  #       semiprime = i*j
  #       semiprimes << semiprime if semiprime > first
  #     end
  #   end
  # end

  def add(new, old)
    products = []
    new.each do |i|
      (new + old).each do |j|
        products << i * j
      end
    end

    products
  end
end

class Semiprimes
  def initialize
    # set of unique semiprimes sorted in ascending order
    @set = Set.new

    # key: semiprime, value: location in the array
    @hash = {}
  end

  # def append(number)
  #   last = set.last || 0
  #   return if number <= last
  #
  #   find_semiprimes(last, number)
  # end
  # alias_method :<<, :append

  def add(new)
    set = (set + new).sort

    hash.clear
    set.each_with_index do |semiprime, index|
      hash[semiprime] = index
    end
  end

  private

  attr_reader :set, :hash
end

# class SemiprimeFinder
#   attr_reader :number, :primes
#
#   def initialize(number)
#     @number = number
#     @primes = Primes.new
#     # @array = Array.new(count, 0)
#   end
#
#   def primes(number)
#     largest_prime = number / 2
#     primes << largest_prime
#   end
# end

def solution(n, p, q)
  # write your code in Ruby 2.2
  primes = Primes.new
  result = []
  p.each_with_index do |first, index|
    last = q[index]
    primes << first / 2
    primes << last / 2

    result << primes.semiprimes[last] - primes.semiprimes[first]
  end

  puts result
end
