@.str = private unnamed_addr constant [3 * i8] c"teste\00", align 1 
; Function Attrs; noinline nounwind optnone uwtable 
define dso_local i32 @main() #0 { 
%1 = alloca i32, align 4
%2 = alloca i32, align 4
%3 = alloca i32, align 4
%2 = store i32 , i32* %2, align 4
%2 = add i32 %2, 5
%1 = store i32 %2, i32* %1, align 4
%1 = sub i32 %1, 1336
call i32 (i8*, ...) @printf( i8* %1 ) nounwind
}
declare dso_local i32 @printf(i8*, ...) #1
