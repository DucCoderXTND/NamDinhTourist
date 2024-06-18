using System.Linq.Expressions;

namespace TND.Application.Extensions
{
    /// <summary>
    /// Cung cấp các phương thức mở rộng cho các đối tượng <see cref='Expression' /> object.
    ///  mục đích của nó là kết hợp hai biểu thức điều kiện (Expression<Func<T, bool>>) lại với nhau bằng toán tử logic "AND" (&&).
    ///</summary>
    public static class ExpressionExtensions
    {
        /// <summary> 

        /// Kết hợp hai đối tượng <see cref="Expression{TDelegate}" /> thành một biểu thức đơn 

        /// đại diện cho phép thực hiện AND logic giữa chúng. 

        /// </summary> 

        /// <typeparam name="T">Kiểu của tham số trong các biểu thức.</typeparam> 

        /// <param name="left">Biểu thức bên trái.</param> 

        /// <param name="right">Biểu thức bên phải.</param> 

        /// <returns> 

        /// Một <see cref="Expression{TDelegate}" /> mới đại diện cho phép thực hiện AND logic 

        /// giữa các biểu thức được cung cấp. 

        /// </returns> 

        /// <remarks> 

        /// Phương thức này kết hợp các thân của các biểu thức được cung cấp bằng cách sử dụng toán tử AND (&&). 

        /// Nó tạo ra một biểu thức lambda mới với thân được kết hợp và các tham số từ biểu thức bên trái. 

        /// </remarks>

        //ex Giả sử bạn có hai biểu thức điều kiện như sau:
        //Expression<Func<int, bool>> expr1 = x => x > 5;
        //Expression<Func<int, bool>> expr2 = x => x < 10;

        //Khi sử dụng phương thức And để kết hợp hai biểu thức này:
        //var combinedExpr = expr1.And(expr2);

        //Biểu thức combinedExpr sẽ tương đương với:
        //    x => x > 5 && x< 10;

        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var andExpression = Expression.AndAlso(left.Body,
                Expression.Invoke(right, left.Parameters[0]));

            var lambda = Expression.Lambda<Func<T, bool>>(andExpression, left.Parameters);
            return lambda;
        }
    }
}
