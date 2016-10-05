<div id="brinza-task-description">
<p>A <i>word machine</i> is a system that performs a sequence of simple operations on a stack of integers. Initially the stack is empty. The sequence of operations is given as a string. Operations are separated by single spaces. The following operations may be specified:</p>
<blockquote><ul style="margin: 10px;padding: 0px;"><li>an integer X (between 0 and 2<sup>20</sup> − 1): the machine pushes X onto the stack;</li>
<li>"<tt style="white-space:pre-wrap">POP</tt>": the machine removes the topmost number from the stack;</li>
<li>"<tt style="white-space:pre-wrap">DUP</tt>": the machine pushes a duplicate of the topmost number onto the stack;</li>
<li>"<tt style="white-space:pre-wrap">+</tt>": the machine pops the two topmost elements from the stack, adds them together and pushes the sum onto the stack;</li>
<li>"<tt style="white-space:pre-wrap">-</tt>": the machine pops the two topmost elements from the stack, subtracts the second one from the first (topmost) one and pushes the difference onto the stack.</li>
</ul>
</blockquote><p>After processing all the operations, the machine returns the topmost value from the stack.</p>
<p>The machine processes 20-bit unsigned integers (numbers between 0 and 2<sup>20</sup> − 1). An overflow in addition or underflow in subtraction causes an error. The machine also reports an error when it tries to perform an operation that expects more numbers on the stack than the stack actually contains. Also, if, after performing all the operations, the stack is empty, the machine reports an error.</p>
<p>For example, given a string "<tt style="white-space:pre-wrap">13 DUP 4 POP 5 DUP + DUP + -</tt>", the machine performs the following operations:</p>
<tt style="white-space:pre-wrap">  operation | comment             | stack
--------------------------------------------------
|                     | [empty]
"13"       | push 13             |
|                     | 13
"DUP"      | duplicate 13        |
|                     | 13, 13
"4"        | push 4              |
|                     | 13, 13, 4
"POP"      | pop 4               |
|                     | 13, 13
"5"        | push 5              |
|                     | 13, 13, 5
"DUP"      | duplicate 5         |
|                     | 13, 13, 5, 5
"+"        | add 5 and 5         |
|                     | 13, 13, 10
"DUP"      | duplicate 10        |
|                     | 13, 13, 10, 10
"+"        | add 10 and 10       |
|                     | 13, 13, 20
"-"        | subtract 13 from 20 |
|                     | 13, 7</tt>
<p>Finally, the machine will return 7.</p>
<p>Given a string "<tt style="white-space:pre-wrap">5 6 + -</tt>", the machine reports an error, since, after the addition, there is only one number on the stack, but the subtraction operation expects two.</p>
<p>Given a string "<tt style="white-space:pre-wrap">3 DUP 5 - -</tt>", the machine reports an error, since the second subtraction yields a negative result.</p>
<p>Write a function:</p>
<blockquote><p style="font-family: monospace; font-size: 9pt; display: block; white-space: pre-wrap"><tt>def solution(s)</tt></p></blockquote>
<p>that, given a string S containing a sequence of operations for the word machine, returns the result the machine would return after processing the operations. The function should return −1 if the machine would report an error while processing the operations.</p>
<p>For example, given string S = "<tt style="white-space:pre-wrap">13 DUP 4 POP 5 DUP + DUP + -</tt>" the function should return 7, as explained in the example above. Given string S = "<tt style="white-space:pre-wrap">5 6 + -</tt>" or S = "<tt style="white-space:pre-wrap">3 DUP 5 - -</tt>" the function should return −1.</p>
<p>Assume that:</p>
<blockquote><ul style="margin: 10px;padding: 0px;"><li>the length of S is within the range [<span class="number">0</span>..<span class="number">2,000</span>];</li>
<li>S is a valid sequence of word machine operations.</li>
</ul>
</blockquote><p>In your solution, focus on <b><b>correctness</b></b>. The performance of your solution will not be the focus of the assessment.</p>
</div>
