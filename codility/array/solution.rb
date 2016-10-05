class Controller
  attr_reader :counter

  def initialize(numbers)
    set_count(numbers)
  end

  def pairs_per_number
    pairs = Hash.new(0)
    @counter.each do |number, count|
      # Match pairs where the later appears after the former
      #       (1..count-1).each do |i|
      #               pairs[number] += i
      #                     end
      #                         end
      #                             
      #                                 pairs
      #                                   end
      #                                     
      #                                       private
      #                                         
      #                                           def set_count(numbers)
      #                                               @counter = Hash.new(0)
      #                                                   Array(numbers).each do |number|
      #                                                         @counter[number] += 1
      #                                                             end
      #                                                               end
      #                                                               end
      #
      #                                                               def solution(a)
      #                                                                 pairs_per_number = Controller.new(a).pairs_per_number
      #                                                                   
      #                                                                     pairs_count = 0
      #                                                                       pairs_per_number.each do |number, pairs|
      #                                                                           pairs_count += pairs
      #                                                                             end
      #                                                                               
      #                                                                                 pairs_count > 1000000000 ? 1000000000 : pairs_count
      #                                                                                 end
    end
  end
end
end
